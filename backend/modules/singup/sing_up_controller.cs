
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

    public class SingUpController : ControllerBase
    {
        private SingUpDataSource _userrepository;
        private EmailSend _emailsend;

        private EmailSmtp _emailsmtp;
        public SingUpController(SingUpDataSource repository,
                                EmailSend emailSend,
                                EmailSmtp emailSmtp)
        {
            this._userrepository = repository;
            this._emailsend = emailSend;
            this._emailsmtp = emailSmtp;
        }

        [HttpGet("ap1/confirmation-email/{email}")]
        [Produces("text/html")]
        public IActionResult GetConfirmationEmail(
            [FromRoute] string email
        )
        {
            try
            {
                var nome = _userrepository.ConfirmationEmail(email);

                return Ok(ReplaceHtmlService.ReplaceHtml(nome, "\\template\\email\\welcome-email.html", "NAME-BD"));
            }
            catch (EmailConfirmationException err)
            {
                return Ok(err.Message);
            }

        }

        [HttpPost("api1/register")]
        public async Task<IActionResult> PostRegister(
            [FromBody] UserRegisterRequest userModelRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var query = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;
                    return BadRequest(new ResultResponse<UserRegisterRequest>(query.ToList()));
                }

                _userrepository.CreateUser(userModelRequest);

                await _emailsmtp.executeAsync(userModelRequest.email, "Email de verificação");

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
    }
}