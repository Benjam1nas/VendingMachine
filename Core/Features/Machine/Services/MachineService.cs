using Core.Features.Machine.Requests;
using Core.Features.Machine.Responses;
using Core.Interfaces.Services;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using static Core.Features.Machine.Responses.BuyMachineItemResponse;

namespace Core.Features.Machine.Services;

public class MachineService : IMachineService
{
    private readonly ApplicationDbContext _db;

    public MachineService(ApplicationDbContext db)
    {
        _db = db;
    }

    private static MachineResponseDto MapToDto(Domain.Models.Machine machine)
    {
        return new MachineResponseDto
        {
            Id = machine.Id,
            Title = machine.Title,
            Location = machine.Location,
            ItemList = machine.Items.Select(i => new MachineItemDto
            {
                ItemId = i.ItemId,
                Title = i.Item?.Title ?? "Unknown",
                Cost = i.Item?.Cost ?? 0,
                Quantity = i.Quantity
            }).ToList(),
            CoinList = machine.Coins.Select(c => new MachineCoinDto
            {
                CoinId = c.CoinId,
                Value = c.Coin?.Value ?? 0,
                Quantity = c.Quantity
            }).ToList()
        };
    }

    public async Task<MachineResponseDto> CreateMachineAsync(CreateMachineRequest request)
    {
        var machine = new Domain.Models.Machine
        {
            Title = request.Title,
            Location = request.Location
        };

        _db.Machines.Add(machine);
        await _db.SaveChangesAsync();

        return MapToDto(machine);
    }

    public async Task<MachineResponseDto> FillMachineCoinsAsync(int machineId, FillMachineCoinRequest request)
    {
        var machine = await _db.Machines
            .Include(m => m.Coins)
                .ThenInclude(m => m.Coin)
            .FirstOrDefaultAsync(m => m.Id == machineId);

        if (machine == null)
        {
            throw new Exception("Machine not found");
        }

        foreach (var coin in request.Coins)
        {
            var existingCoin = machine.Coins.FirstOrDefault(c => c.CoinId == coin.CoinId);

            if (existingCoin != null)
            {
                existingCoin.Quantity += coin.Quantity;
            }
            else
            {
                machine.Coins.Add(new Domain.Models.MachineCoin
                {
                    MachineId = machine.Id,
                    CoinId = coin.CoinId,
                    Quantity = coin.Quantity
                });
            }
        }

        await _db.SaveChangesAsync();
        return MapToDto(machine);
    }

    public async Task<MachineResponseDto> FillMachineItemsAsync(int machineId, FillMachineItemRequest request)
    {
        var machine = await _db.Machines
           .Include(m => m.Items)
                .ThenInclude(m => m.Item)
           .FirstOrDefaultAsync(m => m.Id == machineId);

        if (machine == null)
        {
            throw new Exception("Machine not found");
        }

        foreach (var item in request.Items)
        {
            var existingItem = machine.Items.FirstOrDefault(i => i.ItemId == item.ItemId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                machine.Items.Add(new Domain.Models.MachineItem
                {
                    MachineId = machine.Id,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity
                });
            }
        }

        await _db.SaveChangesAsync();
        return MapToDto(machine);
    }
    public async Task<BuyMachineItemResponse> RequestMachineItemAsync(BuyMachineItemRequest request)
    {
        var machine = await _db.Machines
            .Include(m => m.Coins)
                .ThenInclude(c => c.Coin)
            .Include(m => m.Items)
                .ThenInclude(i => i.Item)
            .FirstOrDefaultAsync(m => m.Id == request.MachineId);

        int totalValue = 0;
        int insertedValue = 0;

        var response = new BuyMachineItemResponse();

        if (machine == null)
        {
            throw new Exception("Machine not found");
        }

        foreach (var item in request.Items)
        {
            var existingItem = machine.Items.FirstOrDefault(i => i.ItemId == item.ItemId);

            if (existingItem == null)
            {
                throw new Exception("Machine item is not found");
            }

            if (existingItem.Quantity < item.Quantity)
            {
                throw new Exception("Machine is out of this item or too much quantity is requested");
            }

            existingItem.Quantity -= item.Quantity;
            totalValue += existingItem.Item.Cost * item.Quantity;

            response.Items.Add(new ReturnedItem
            {
                ItemId = item.ItemId,
                Title = existingItem.Item.Title,
                Quantity = item.Quantity
            });
        }

        foreach (var coin in request.Coins)
        {
            var existingCoin = machine.Coins.FirstOrDefault(c => c.CoinId == coin.CoinId);

            if (existingCoin == null) // Užregistruojam naują monetą, jeigu aparatas tokios neturi
            {
                var newCoin = await _db.Coins.FirstOrDefaultAsync(c => c.Id == coin.CoinId) ?? throw new Exception("Invalid coin");

                existingCoin = new MachineCoin
                {
                    CoinId = coin.CoinId,
                    Coin = newCoin,
                    MachineId = machine.Id,
                    Quantity = 0
                };

                machine.Coins.Add(existingCoin);
            }

            existingCoin.Quantity += coin.Quantity;
            insertedValue += existingCoin.Coin.Value * coin.Quantity;
        }

        if (insertedValue < totalValue)
        {
            throw new Exception("Inserted amount is not enough");
        }

        int changeNeeded = insertedValue - totalValue;

        var coinsForChange = new List<CoinChange>();

        foreach (var coin in machine.Coins.OrderByDescending(c => c.Coin?.Value))
        {
            int coinValue = coin.Coin.Value;
            int needed = changeNeeded / coinValue;

            if (needed <= 0)
            {
                continue;
            }

            int toUse = Math.Min(needed, coin.Quantity);

            if (toUse > 0)
            {
                coinsForChange.Add(new CoinChange
                {
                    CoinId = coin.CoinId,
                    Value = coinValue,
                    Quantity = toUse
                });

                coin.Quantity -= toUse;
                changeNeeded -= coinValue * toUse;
            }

            if (changeNeeded == 0)
                break;
        }

        if (changeNeeded > 0)
        {
            throw new Exception("Machine won't be able to give change");
        }

        await _db.SaveChangesAsync();
        response.Change = coinsForChange;

        return response;
    }
}
