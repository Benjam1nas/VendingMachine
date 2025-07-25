using Core.Features.Coin.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

[ApiController]
[Route("api/coins")]
public class CoinController : Controller
{
    private readonly ICoinService _coinService;

    public CoinController(ICoinService coinService)
    {
        _coinService = coinService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCoinRequest request)
    {
        var result = await _coinService.CreateCoinAsync(request);
        return Ok(result);
    }
}
