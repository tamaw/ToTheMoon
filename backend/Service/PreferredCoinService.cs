using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Models;

namespace ToTheMoon.Api.Service
{
    public class PreferredCoinService : IChangePreferredCoinService
    {
        public Result<ChangePreferredCoinRequest> ValidateChangeRequest(ChangePreferredCoinRequest request)
        {
            return Result<ChangePreferredCoinRequest>.Success(request);
        }
    }
}