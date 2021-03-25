using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api
{
    public class CointreeHttpClient
    {
        private HttpClient Client { get; }
        private JsonSerializerOptions Options { get; }

        public CointreeHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://trade.cointree.com");
            Client = client;

            Options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<CointreePriceResponse> GetCointreeCoinData(string coin)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/prices/AUD/{coin}");

            var response = await Client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CointreePriceResponse>(content, Options);

            return result;
        }
    }
}