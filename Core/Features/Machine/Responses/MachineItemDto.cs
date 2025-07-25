namespace Core.Features.Machine.Responses;

public class MachineItemDto
{
    public int ItemId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Cost { get; set; }
    public int Quantity { get; set; }
}
