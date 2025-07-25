using Core.Features.Item.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

[ApiController]
[Route("api/items")]
public class ItemController : Controller
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemRequest request)
    {
        var result = await _itemService.CreateItemAsync(request);
        return Ok(result);
    }
}
