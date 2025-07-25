using Core.Features.Coin.Requests;
using Core.Features.Coin.Responses;

namespace Core.Interfaces.Services
{
    public interface ICoinService
    {
        Task<CoinResponseDto> CreateCoinAsync(CreateCoinRequest request);
    }
}