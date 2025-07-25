using Domain.Models;

namespace Core.Features.Machine.Responses;

public class MachineResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Location {  get; set; } = string.Empty;
    public List<MachineCoinDto> CoinList { get; set; } = [];
    public List<MachineItemDto> ItemList { get; set; } = [];
}
