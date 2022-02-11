
using backend.db;
using backend.exception;
using backend.model;
using backend.requestmodels;
using backend.response;
using backend.settings;
using backend.UseCases;
using backend.UseCases.model;
using backend.UseCases.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    public class SinginController : ControllerBase
    {
        private SingInDataSource _userrepository;
        private TokenService _auth;
        public SinginController(SingInDataSource repository, TokenService auth)
        {
            this._userrepository = repository;
            this._auth = auth;
        }

        [HttpPost("api1/login")]
        public IActionResult PostLogin(
            [FromBody] UserLoginRequest user
        )
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;
                return BadRequest(new ResultResponse<UserLoginRequest>(query.ToList()));
            }
            try
            {
                return Ok(new ResultResponse<UserLoginResponse>(((IGetLoginUser)_userrepository).GetUserLogin(user)));
            }
            catch (ValidateException err)
            {
                return BadRequest(new ResultResponse<UserLoginResponse>(err.Message));
            }
            catch (EmailConfirmationException err)
            {
                return StatusCode(412, new ResultResponse<UserLoginResponse>(err.Message));
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }

        }
        [HttpPost("api1/RefreshToken")]
        public IActionResult RefreshToken(
            [FromBody] TokenRequest tokenRequest
            )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _auth.VerifyToken(tokenRequest);

                    if (res == null)
                    {
                        return BadRequest(new ResultResponse<UserLoginResponse>("Token inválido"));
                    }

                    return Ok(res);
                }
                var query = from state in ModelState.Values
                            from error in state.Errors
                            select error.ErrorMessage;
                return BadRequest(new ResultResponse<UserLoginRequest>(query.ToList()));

            }
            catch (ValidateException exeption)
            {
                return BadRequest(new ResultResponse<UserLoginRequest>(exeption.Message));
            }
            catch (Exception err)
            {
                return StatusCode(500, "Erro 12122183128x18821 contate o desenvolvedor");
            }
        }
    }
}