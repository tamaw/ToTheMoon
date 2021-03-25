using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using ToTheMoon.Api.Models;
using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Extensions;

namespace ToTheMoon.Api.Controllers
{

    [ApiController, Route("api/{controller}")]
    public class CoinPriceController : ControllerBase
    {
        public ICoinPriceService CoinPriceService { get; }

        public CoinPriceController(ICoinPriceService coinPriceService) {
            CoinPriceService = coinPriceService;
        }

        [HttpGet("{coin}")]
        public async Task<IActionResult> GetAskPriceAsync([FromRoute] string coin) =>
            await CoinPriceService.ValidateRequest(coin)
                .OnSuccess(coin => CoinPriceService.GetCoinDataAsync(coin))
                .OnSuccess(coinData => CoinPriceService.MapToResponse(coinData))
                .Handle(this);
    }
}