using Domain.Models.Base;

namespace Domain.Models;

public class MachineCoin : BaseModel
{
    public int MachineId { get; set; }
    public Machine? Machine { get; set; }

    public int CoinId { get; set; }
    public Coin? Coin { get; set; }

    public int Quantity { get; set; }
}
