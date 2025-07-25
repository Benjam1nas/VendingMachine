using static Core.Features.Machine.Requests.FillMachineCoinRequest;
using static Core.Features.Machine.Requests.FillMachineItemRequest;

namespace Core.Features.Machine.Requests;

public class BuyMachineItemRequest
{
    public required int MachineId { get; set; }
    public required List<FillItem> Items { get; set; }
    public required List<FillCoin> Coins { get; set; }
}
