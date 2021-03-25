using System;
using System.Linq;
using System.Net.Http;
using ToTheMoon.Api.Interfaces;
using System.Threading.Tasks;
using System.Text.Json;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Service
{
    public class CoinPriceService : ICoinPriceService
    {
        private CointreeHttpClient CointreeHttpClient { get; }
        private readonly string[] acceptedCoins = new string[] { "BTC", "ETH", "XRP" };

        public CoinPriceService(CointreeHttpClient httpClient)
        {
            CointreeHttpClient = httpClient;
        }

        public Result<string> ValidateRequest(string coin)
        {
            if (string.IsNullOrWhiteSpace(coin))
                return Result<string>.Failed(FaultCode.CoinNotProvided);

            if (!acceptedCoins.Contains(coin.Trim().ToUpper()))
                return Result<string>.Failed(FaultCode.CoinUnknownOrNotAccepted);

            return Result<string>.Success(coin);
        }

        public async Task<Result<CointreePriceResponse>> GetCoinDataAsync(string coin)
        {
            try
            {
                var coinData = await CointreeHttpClient.GetCointreeCoinData(coin);

                return Result<CointreePriceResponse>.Success(coinData);
            }
            catch (HttpRequestException)
            {
                return Result<CointreePriceResponse>.Failed(FaultCode.ErrorRetrievingCoinPrice);
            }
            catch (JsonException)
            {
                return Result<CointreePriceResponse>.Failed(FaultCode.FailedToReadResponseData);
            }
            catch
            {
                return Result<CointreePriceResponse>.Failed(FaultCode.Fail);
            }
        }

        public Result<CoinPriceResponse> MapToResponse(CointreePriceResponse cointreeResponse)
        {
            return Result<CoinPriceResponse>.Success(new CoinPriceResponse {
                Ask = cointreeResponse.Ask,
                Bid = cointreeResponse.Bid,
                Rate = cointreeResponse.Rate
            });
        }
    }
}