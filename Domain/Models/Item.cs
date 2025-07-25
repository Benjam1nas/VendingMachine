using Domain.Models.Base;

namespace Domain.Models;

public class Item : BaseModel
{
    public required string Title { get; set; }
    public required int Cost { get; set; }
    public ICollection<MachineItem> Machines { get; set; } = new List<MachineItem>();
}
