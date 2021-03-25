using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Controllers
{

    [ApiController, Route("api/{controller}")]
    public class CoinPriceController : ControllerBase
    {
        private HttpClient CointreeHttpClient { get; }

        public CoinPriceController(IHttpClientFactory clientFactory) {
            CointreeHttpClient = clientFactory.CreateClient("cointree");
        }

        [HttpGet("{coin}") ]
        public async Task<IActionResult> GetAskPriceAsync([FromRoute] string coin) {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/prices/AUD/{coin}");

            var response = await CointreeHttpClient.SendAsync(request);

            if(response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var result = JsonSerializer.Deserialize<CointreePriceResponse>(content, options);

                return Ok(new {
                    AskPrice = result.Ask
                });
            }

            return BadRequest("Did not work lol");
        }

    }
}