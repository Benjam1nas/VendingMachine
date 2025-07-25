using Core.Features.Item.Requests;
using Core.Features.Item.Responses;

namespace Core.Interfaces.Services
{
    public interface IItemService
    {
        Task<ItemResponseDto> CreateItemAsync(CreateItemRequest request);
    }
}