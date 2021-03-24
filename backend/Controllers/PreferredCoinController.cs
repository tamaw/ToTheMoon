using Microsoft.AspNetCore.Mvc;
using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Models;
using ToTheMoon.Api.Extensions;
using Microsoft.AspNetCore.Http;

namespace ToTheMoon.Api.Controllers
{
    [ApiController, Route("api/{controller}")]
    public class PreferredCoinController : ControllerBase
    {
        private IChangePreferredCoinService PreferredCoinService { get; }

        public PreferredCoinController(IChangePreferredCoinService preferredCoinService) {
            PreferredCoinService = preferredCoinService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ChangePreferredCoinRequest request)
                    => PreferredCoinService.ValidateChangeRequest(request)
                .OnSuccess(request => PreferredCoinService.SavePreferredCoin(request))
                .OnSuccess(request => PreferredCoinService.MapToChangePreferredCoinResponse(request))
                .Handle(this);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAction()
        {
            return Ok( new {
                preferredCoin = PreferredCoinService.UserPreferredCoinOrDefault()
            });
        }
    }
}