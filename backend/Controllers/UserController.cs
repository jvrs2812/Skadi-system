
using backend.db;
using backend.model;
using backend.requestmodels;
using backend.response;
using backend.settings;
using backend.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    public class UserController : ControllerBase
    {
        [HttpPost("api1/register")]
        public async Task<IActionResult> PostRegister(
            [FromBody] UserRequest userModelRequest,
            [FromServices] SKADIDBContext context
, DateTime dateTime)
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

                var emailIsNotValid = context.User.FirstOrDefault(x => x.Email == userModelRequest.Email) != null;

                if (emailIsNotValid)
                {
                    return BadRequest(new ResultResponse<UserRequest>("Email já utilizado"));
                }

                var CnpjIsNotValid = context.User.FirstOrDefault(x => x.CNPJ == userModelRequest.CNPJ) != null;

                if (CnpjIsNotValid)
                {
                    return BadRequest(new ResultResponse<UserRequest>("CNPJ já utilizado"));
                }

                var UserRegister = new User();

                UserRegister.CreatedAt = DateTime.UtcNow;
                UserRegister.Email = userModelRequest.Email;
                UserRegister.CNPJ = userModelRequest.CNPJ;
                UserRegister.Name = userModelRequest.Name;
                UserRegister.Password = userModelRequest.Password;
                UserRegister.Id = GetHashCode().ToString();
                UserRegister.Role = "";

                context.User.Add(UserRegister);
                context.SaveChanges();

                var UserResponse = new UserRegisterResponse();
                var token = TokenService.GenerateToken(UserRegister);

                UserResponse.Name = UserRegister.Name;
                UserResponse.Id = UserRegister.Id;
                UserResponse.Email = UserRegister.Email;
                UserResponse.CNPJ = UserRegister.CNPJ;
                UserResponse.Token = token;

                return Created($"api1/login", new ResultResponse<UserRegisterResponse>(UserResponse));
            }
            catch (Exception err)
            {
                return StatusCode(500, new ResultResponse<User>("Erro interno 31231x123 contate o desenvolvedor"));
            }
        }
        [HttpPost("api1/login")]
        public async Task<IActionResult> PostLogin(
            [FromBody] UserRequest user,
            [FromServices] SKADIDBContext context
        )
        {


            return Ok();
        }

        [HttpGet("api1/user/{id}")]
        public async Task<IActionResult> GetUser(
            [FromRoute] String id,
            [FromServices] SKADIDBContext context
        )
        {
            try
            {
                var user = context.User.FirstOrDefault(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultResponse<User>("Usuário não encontrado."));

                return Ok(new ResultResponse<User>(user));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultResponse<User>("Erro 50920x989 relatar ao desenvolvedor"));
            }

        }
    }
}