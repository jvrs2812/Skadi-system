using backend.db;
using backend.exception;
using backend.model;
using backend.requestmodels;
using backend.response;
using backend.settings;
using Microsoft.AspNetCore.Identity;
using SecureIdentity.Password;

namespace backend.repository.user
{
    public class UserRepository
    {
        private SKADIDBContext _context;
        private UserRequest _userrequest;

        public UserRepository(SKADIDBContext context, UserRequest userrequest)
        {
            this._context = context;
            this._userrequest = userrequest;
        }

        public UserRepository(SKADIDBContext context, UserLoginRequest userLoginRequest)
        {
            this._context = context;
            this._userrequest = new UserRequest();
            this._userrequest.email = userLoginRequest.email;
            this._userrequest.password = userLoginRequest.password;
        }
        public void CreateUserFather()
        {
            var userCreate = new User();

            userCreate.id = Guid.NewGuid().ToString();
            userCreate.name = _userrequest.name;
            userCreate.CreatedAt = DateTime.Now;
            userCreate.Role = "";
            userCreate.password = PasswordHasher.Hash(_userrequest.password);
            userCreate.email = _userrequest.email;

            _context.User.Add(userCreate);
            _context.SaveChanges();
        }

        public void ValidateUser()
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

            var token = TokenService.GenerateToken(user);
            var UserResponse = new UserLoginResponse();

            UserResponse.name = user.name;
            UserResponse.id = user.id;
            UserResponse.email = user.email;
            UserResponse.token = token;

            return UserResponse;
        }
    }
}