using backend.requestmodels;
using backend.response;

interface IGetLoginUser
{
    public UserLoginResponse GetUserLogin(UserLoginRequest request);
}