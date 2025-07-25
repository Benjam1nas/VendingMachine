namespace Core.Features.Machine.Requests;

public class FillMachineItemRequest
{
    public class FillItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
    public List<FillItem> Items { get; set; } = [];
}
