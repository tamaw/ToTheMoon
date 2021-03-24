using Microsoft.AspNetCore.Mvc;
using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Models;
using ToTheMoon.Api.Extensions;

namespace ToTheMoon.Api.Controllers
{
    [ApiController, Route("api/{controller}")]
    public class PreferredCoinController : ControllerBase
    {
        public IChangePreferredCoinService PreferredCoinService { get; }

        public PreferredCoinController(IChangePreferredCoinService preferredCoinService) {
            PreferredCoinService = preferredCoinService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ChangePreferredCoinRequest request)
                    => PreferredCoinService.ValidateChangeRequest(request)
                .Handle(this);
    }
}