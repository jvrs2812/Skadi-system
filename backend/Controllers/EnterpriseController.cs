using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class EnterpriseController : ControllerBase
    {

        [HttpGet("api1/enterprises/{id}")]
        public IActionResult getInterpriseUser(
            [FromRoute] string id
        )
        {

            
            return Ok();
        }

    }
}