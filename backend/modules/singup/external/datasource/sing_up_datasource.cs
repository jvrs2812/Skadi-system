using backend.db;
using backend.exception;
using backend.model;
using backend.requestmodels;
using Microsoft.AspNetCore.Identity;
using SecureIdentity.Password;

public class SingUpDataSource : ICreateUser
{

    private SKADIDBContext _db;
    public SingUpDataSource(SKADIDBContext dbcontext)
    {
        this._db = dbcontext;
    }
    public void CreateUser(UserRegisterRequest userrequest)
    {
        try
        {
            var userCreate = new User();

            userCreate.id = Guid.NewGuid().ToString();
            userCreate.name = userrequest.name;
            userCreate.CreatedAt = DateTime.Now;
            userCreate.password = PasswordHasher.Hash(userrequest.password);
            userCreate.email = userrequest.email;
            userCreate.emailConfirmation = false;

            ValidateUser(userCreate);

            _db.User.Add(userCreate);
            _db.SaveChanges();
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
    private void ValidateUser(User userrequest)
    {
        var emailIsNotValid = _db.User.FirstOrDefault(x => x.email == userrequest.email) != null;

        if (emailIsNotValid)
        {
            throw new ValidateException("Email já utilizado.");
        }
    }
    public string ConfirmationEmail(String email)
    {
        var user = _db.User.Where(x => x.email == email).Where(y => y.emailConfirmation == false).FirstOrDefault();

        if (user == null)
            throw new EmailConfirmationException("Email não confirmado");

        user.emailConfirmation = true;

        _db.User.Update(user);
        _db.SaveChanges();

        return user.name;
    }
}