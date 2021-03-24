using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Interfaces
{
    public interface IChangePreferredCoinService
    {
        Result<ChangePreferredCoinRequest> ValidateChangeRequest(ChangePreferredCoinRequest request);
        Result<ChangePreferredCoinRequest> SavePreferredCoin(ChangePreferredCoinRequest request);
        Result<ChangePreferredCoinResponse> MapToChangePreferredCoinResponse(ChangePreferredCoinRequest request);
    }
}
