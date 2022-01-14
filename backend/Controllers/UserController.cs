using backend.db;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    public class UserController : ControllerBase
    {
        [HttpGet("api1/user")]
        public async Task<IActionResult> GetUser(
            [FromServices] SKADIDBContext context
        )
        {
            return Ok(context.User.ToList());
        }
    }
}