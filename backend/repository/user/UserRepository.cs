using backend.db;
using backend.exception;
using backend.model;
using backend.requestmodels;
using backend.response;
using backend.settings;
using backend.UseCases.services;
using SecureIdentity.Password;

namespace backend.repository.user
{
    public class UserRepository
    {
        private SKADIDBContext _context;
        private UserRegisterRequest _userrequest;

        public UserRepository(SKADIDBContext context, UserRegisterRequest userrequest)
        {
            this._context = context;
            this._userrequest = userrequest;
        }
        public UserRepository(SKADIDBContext context, string email)
        {
            this._context = context;
            this._userrequest = new UserRegisterRequest();
            this._userrequest.email = email;
        }


        public UserRepository(SKADIDBContext context, UserLoginRequest userLoginRequest)
        {
            this._context = context;
            this._userrequest = new UserRegisterRequest();
            this._userrequest.email = userLoginRequest.email;
            this._userrequest.password = userLoginRequest.password;
        }
        public void CreateUser()
        {
            try
            {
                var userCreate = new User();

                userCreate.id = Guid.NewGuid().ToString();
                userCreate.name = _userrequest.name;
                userCreate.CreatedAt = DateTime.Now;
                userCreate.Role = "";
                userCreate.password = PasswordHasher.Hash(_userrequest.password);
                userCreate.email = _userrequest.email;
                userCreate.emailConfirmation = false;
                ValidateUser();

                _context.User.Add(userCreate);
                _context.SaveChanges();
            }
            catch (ValidateException err)
            {
                throw new ValidateException(err.Message);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        private void ValidateUser()
        {
            var emailIsNotValid = _context.User.FirstOrDefault(x => x.email == _userrequest.email) != null;

            if (emailIsNotValid)
            {
                throw new ValidateException("Email já utilizado.");
            }
        }

        public UserLoginResponse Login()
        {
            var user = _context.User.FirstOrDefault(x => x.email == _userrequest.email);

            if (user == null)
                throw new ValidateException("Email ou senha não encontrado");

            if (!PasswordHasher.Verify(user.password, _userrequest.password))
                throw new ValidateException("Email ou senha não encontrado");

            if (!user.emailConfirmation)
                throw new EmailConfirmationException("Email não confirmado");

            var token = TokenService.GenerateToken(user);
            var UserResponse = new UserLoginResponse();

            UserResponse.name = user.name;
            UserResponse.id = user.id;
            UserResponse.email = user.email;
            UserResponse.token = token;

            return UserResponse;
        }

        public string ConfirmationEmail()
        {
            var user = _context.User.Where(x => x.email == _userrequest.email).Where(y => y.emailConfirmation == false).FirstOrDefault();

            if (user == null)
                throw new EmailConfirmationException("Email não confirmado");

            user.emailConfirmation = true;

            _context.User.Update(user);
            _context.SaveChanges();

            return user.name;
        }
    }
}