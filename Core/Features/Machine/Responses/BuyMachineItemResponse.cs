using System.Net;

namespace Core.Features.Machine.Responses;

public class BuyMachineItemResponse
{
    public class ReturnedItem
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }

    public class CoinChange
    {
        public int CoinId { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
    }
    public List<ReturnedItem> Items { get; set; } = [];
    public List<CoinChange> Change { get; set; } = [];
}
