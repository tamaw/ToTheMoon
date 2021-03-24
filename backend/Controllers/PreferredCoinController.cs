using Microsoft.AspNetCore.Mvc;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Controllers
{
    [ApiController, Route("api/{controller}")]
    public class PreferredCoinController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] ChangePreferredCoinRequest request)
        {
            return Ok(new {data = "Got " + request.Coin});
        }
    }
}