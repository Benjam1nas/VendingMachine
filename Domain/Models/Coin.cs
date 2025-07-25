using Domain.Models.Base;

namespace Domain.Models;

public class Coin : BaseModel
{
    public required int Value { get; set; }
    public ICollection<MachineCoin> Machines { get; set; } = new List<MachineCoin>();
}
