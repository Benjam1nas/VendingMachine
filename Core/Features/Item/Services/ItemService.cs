using Core.Features.Item.Requests;
using Core.Features.Item.Responses;
using Core.Interfaces.Services;
using Persistence.Context;

namespace Core.Features.Item.Services;

public class ItemService : IItemService
{
    private readonly ApplicationDbContext _db;

    public ItemService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ItemResponseDto> CreateItemAsync(CreateItemRequest request)
    {
        var item = new Domain.Models.Item
        {
            Title = request.Title,
            Cost = request.Cost
        };

        _db.Items.Add(item);
        await _db.SaveChangesAsync();

        return new ItemResponseDto
        {
            Title = item.Title,
            Cost = item.Cost,
        };
    }
}
