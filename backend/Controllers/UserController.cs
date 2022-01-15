
using backend.db;
using backend.exception;
using backend.model;
using backend.repository.user;
using backend.requestmodels;
using backend.response;
using backend.settings;
using backend.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    public class UserController : ControllerBase
    {
        [HttpPost("api1/register")]
        public IActionResult PostRegister(
            [FromBody] UserRequest userModelRequest,
            [FromServices] SKADIDBContext context)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;
                    return BadRequest(new ResultResponse<UserRequest>(query.ToList()));
                }


                var userCreate = new UserRepository(context: context, userModelRequest);

                userCreate.ValidateUser();

                userCreate.CreateUserFather();

                return Created($"api1/login", "");
            }
            catch (ValidateException err)
            {
                return BadRequest(new ResultResponse<User>(err.Message));
            }
            catch
            {
                return StatusCode(500, new ResultResponse<User>("Erro interno 31231x123 contate o desenvolvedor"));
            }
        }

        [HttpPost("api1/login")]
        public IActionResult PostLogin(
            [FromBody] UserLoginRequest user,
            [FromServices] SKADIDBContext context
        )
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;
                return BadRequest(new ResultResponse<UserRequest>(query.ToList()));
            }
            try
            {
                var UserLogin = new UserRepository(context, user);

                var login = UserLogin.Login();

                return Ok(new ResultResponse<UserLoginResponse>(login));
            }
            catch (ValidateException err)
            {
                return BadRequest(new ResultResponse<UserLoginResponse>(err.Message));
            }
            catch
            {
                return StatusCode(500, "Erro 82831x081 contate o desenvolvedor");
            }

        }
        [Authorize]
        [HttpGet("api1/user/{id}")]
        public IActionResult GetUser(
            [FromRoute] String id,
            [FromServices] SKADIDBContext context
        )
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;
                return BadRequest(new ResultResponse<UserGetResponse>(query.ToList()));
            }
            try
            {
                var user = context.User.FirstOrDefault(x => x.id == id);

                if (user == null)
                    return NotFound(new ResultResponse<UserGetResponse>("Usuário não encontrado."));

                var userResponse = new UserGetResponse();

                userResponse.id = user.id;
                userResponse.name = user.name;
                userResponse.email = user.email;


                return Ok(new ResultResponse<UserGetResponse>(userResponse));
            }
            catch
            {
                return StatusCode(500, new ResultResponse<UserGetResponse>("Erro 50920x989 relatar ao desenvolvedor"));
            }

        }

    }
}