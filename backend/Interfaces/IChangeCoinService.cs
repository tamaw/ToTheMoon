using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Interfaces
{
    public interface IChangePreferredCoinService
    {
        Result<ChangePreferredCoinRequest> ValidateChangeRequest(ChangePreferredCoinRequest request);
    }
}
