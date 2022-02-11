using backend.db;
using backend.exception;
using backend.requestmodels;
using backend.response;
using backend.settings;
using Microsoft.AspNetCore.Identity;
using SecureIdentity.Password;

public class SingInDataSource : IGetLoginUser
{
    private SKADIDBContext _db;
    private TokenService _token;

    public SingInDataSource(SKADIDBContext dbcontext, TokenService token)
    {
        this._db = dbcontext;
        this._token = token;

    }
    public UserLoginResponse GetUserLogin(UserLoginRequest request)
    {
        var user = _db.User.FirstOrDefault(x => x.email == request.email);

        if (user == null)
            throw new ValidateException("Email ou senha não encontrado");

        if (!PasswordHasher.Verify(user.password, request.password))
            throw new ValidateException("Email ou senha não encontrado");

        if (!user.emailConfirmation)
            throw new EmailConfirmationException("Email não confirmado");

        var token = _token.GenerateToken(user);
        var UserResponse = new UserLoginResponse();

        UserResponse.name = user.name;
        UserResponse.id = user.id;
        UserResponse.email = user.email;
        UserResponse.token = token.Token;
        UserResponse.RefreshToken = token.RefreshToken;
        UserResponse.ExpiredToken = token.ExpiredDate;

        return UserResponse;
    }
}