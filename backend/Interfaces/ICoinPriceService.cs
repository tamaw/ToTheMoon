using System.Threading.Tasks;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Interfaces
{
    public interface ICoinPriceService
    {
        Result<string> ValidateRequest(string coin);
        Result<CoinPriceResponse> MapToResponse(CointreePriceResponse cointreeResponse);
        Task<Result<CointreePriceResponse>> GetCoinDataAsync(string coin);
    }
}