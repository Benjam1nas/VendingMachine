using Core.Features.Machine.Requests;
using Core.Features.Machine.Responses;

namespace Core.Interfaces.Services
{
    public interface IMachineService
    {
        Task<MachineResponseDto> CreateMachineAsync(CreateMachineRequest request);
        Task<MachineResponseDto> FillMachineCoinsAsync(int machineId, FillMachineCoinRequest request);
        Task<MachineResponseDto> FillMachineItemsAsync(int machineId, FillMachineItemRequest request);
        Task<BuyMachineItemResponse> RequestMachineItemAsync(BuyMachineItemRequest request);
    }
}