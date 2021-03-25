using ToTheMoon.Api.Interfaces;
using ToTheMoon.Api.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ToTheMoon.Api.Service
{
    public class PreferredCoinService : IChangePreferredCoinService
    {
        private IHttpContextAccessor Accessor { get; }
        private ISession Session => Accessor.HttpContext.Session;
        private const string PreferredCoinSessionVariable = "PreferredCoin";
        private const string DefaultCoin = "BTC";
        private readonly string[] acceptedCoins = new string[] { "BTC", "ETH", "XRP" };

        public PreferredCoinService(IHttpContextAccessor  accessor)
        {
            Accessor = accessor;
        }

        public Result<ChangePreferredCoinRequest> ValidateChangeRequest(ChangePreferredCoinRequest request)
        {
            if(string.IsNullOrWhiteSpace(request?.Coin))
                return Result<ChangePreferredCoinRequest>.Failed(FaultCode.CoinNotProvided);

            if(!acceptedCoins.Contains(request.Coin.Trim().ToUpper()))
                return Result<ChangePreferredCoinRequest>.Failed(FaultCode.CoinUnknownOrNotAccepted);

            return Result<ChangePreferredCoinRequest>.Success(request);
        }

        public Result<ChangePreferredCoinRequest> SavePreferredCoin(ChangePreferredCoinRequest request)
        {
            Session.SetString(PreferredCoinSessionVariable, request.Coin);
            return Result<ChangePreferredCoinRequest>.Success(request);
        }

        public Result<ChangePreferredCoinResponse> MapToChangePreferredCoinResponse(ChangePreferredCoinRequest request) =>
                Result<ChangePreferredCoinResponse>.Success(new ChangePreferredCoinResponse
                {
                    ChangedTo = request.Coin.Trim().ToUpper()
                });

        public string UserPreferredCoinOrDefault()
        {
            return Session.GetString(PreferredCoinSessionVariable) ?? DefaultCoin;
        }
    }
}