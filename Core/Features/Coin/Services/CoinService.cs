using Core.Features.Coin.Requests;
using Core.Features.Coin.Responses;
using Core.Interfaces.Services;
using Persistence.Context;

namespace Core.Features.Coin.Services;

public class CoinService : ICoinService
{
    private readonly ApplicationDbContext _db;

    public CoinService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CoinResponseDto> CreateCoinAsync(CreateCoinRequest request)
    {
        var coin = new Domain.Models.Coin // Prielaida, kad laikom monetų reikšmes int tipo: "10, 100" (centų) 
        {
            Value = request.Value
        };

        _db.Coins.Add(coin);
        await _db.SaveChangesAsync();

        return new CoinResponseDto
        {
            Id = coin.Id,
            Value = coin.Value
        };
    }
}
