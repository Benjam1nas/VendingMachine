using Core.Features.Coin.Requests;
using Core.Features.Machine.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

[ApiController]
[Route("api/machines")]
public class MachineController : Controller
{
    private readonly IMachineService _machineService;

    public MachineController(IMachineService machineService)
    {
        _machineService = machineService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMachineRequest request)
    {
        var result = await _machineService.CreateMachineAsync(request);
        return Ok(result);
    }

    [HttpPost("/fill/items/{machineId}")]
    public async Task<IActionResult> FillItem(int machineId, [FromBody] FillMachineItemRequest request)
    {
        var result = await _machineService.FillMachineItemsAsync(machineId, request);
        return Ok(result);
    }

    [HttpPost("/fill/coins/{machineId}")]
    public async Task<IActionResult> FillCoin(int machineId, [FromBody] FillMachineCoinRequest request)
    {
        var result = await _machineService.FillMachineCoinsAsync(machineId, request);
        return Ok(result);
    }

    [HttpPost("/buy")]
    public async Task<IActionResult> FillCoin([FromBody] BuyMachineItemRequest request)
    {
        var result = await _machineService.RequestMachineItemAsync(request);
        return Ok(result);
    }
}
