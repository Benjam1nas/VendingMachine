using Domain.Models.Base;

namespace Domain.Models;

public class Machine : BaseModel
{
    public required string Title { get; set; }
    public required string Location { get; set; }
    public ICollection<MachineItem> Items { get; set; } = new List<MachineItem>();
    public ICollection<MachineCoin> Coins { get; set; } = new List<MachineCoin>();
}
