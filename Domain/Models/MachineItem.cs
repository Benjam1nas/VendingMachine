using Domain.Models.Base;

namespace Domain.Models;

public class MachineItem : BaseModel
{
    public int MachineId { get; set; }
    public Machine? Machine { get; set; }

    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public int Quantity { get; set; }
}
