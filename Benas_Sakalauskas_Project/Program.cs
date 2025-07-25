using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Core.Features.Machine.Services;
using Core.Features.Coin.Services;
using Core.Features.Item.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICoinService, CoinService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IMachineService, MachineService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
