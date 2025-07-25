namespace Core.Features.Machine.Requests;

public class FillMachineCoinRequest
{
    public class FillCoin
    {
        public int CoinId { get; set; }
        public int Quantity { get; set; }
    }
    public List<FillCoin> Coins { get; set; } = [];
}
