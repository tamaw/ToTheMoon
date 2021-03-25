using System;
using System.Linq;
using System.Net.Http;
using ToTheMoon.Api.Interfaces;
using System.Threading.Tasks;
using System.Text.Json;
using ToTheMoon.Api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ToTheMoon.Api.Service
{
    public class CoinPriceService : ICoinPriceService
    {
        private CointreeHttpClient CointreeHttpClient { get; }
        private IMemoryCache MemoryCache { get; }
        private MemoryCacheEntryOptions CacheExpiryOptions { get; }

        private readonly string[] acceptedCoins = new string[] { "BTC", "ETH", "XRP" };

        public CoinPriceService(CointreeHttpClient httpClient, IMemoryCache memoryCache)
        {
            CointreeHttpClient = httpClient;
            MemoryCache = memoryCache;

            CacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(1)
            };
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
                bool exists = MemoryCache.TryGetValue(coin, out CointreePriceResponse coinData);
                if (!exists) {
                    coinData = await CointreeHttpClient.GetCointreeCoinData(coin);
                    MemoryCache.Set(coin, coinData, CacheExpiryOptions);
                }

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