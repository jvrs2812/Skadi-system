using backend.db;
using backend.exception;
using backend.model;
using backend.repository.user;
using backend.UseCases;
using backend.UseCases.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    public class EmailController : ControllerBase
    {
        [HttpGet("ap1/confirmation-email/{email}")]
        [Produces("text/html")]
        public IActionResult GetConfirmationEmail(
            [FromRoute] string email,
            [FromServices] SKADIDBContext context
        )
        {
            try
            {
                var UserLogin = new UserRepository(context, email);

                var nome = UserLogin.ConfirmationEmail();

                return Ok(ReplaceHtmlService.ReplaceHtml(nome, "\\template\\email\\welcome-email.html", "NAME-BD"));
            }
            catch (EmailConfirmationException err)
            {
                return Ok(err.Message);
            }

        }
    }
}