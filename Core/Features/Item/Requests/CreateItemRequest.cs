namespace Core.Features.Item.Requests;

public class CreateItemRequest
{
    public required string Title { get; set; }
    public required int Cost { get; set; } 
}
