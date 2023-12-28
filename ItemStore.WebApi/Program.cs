using ItemStore.WebApi.csproj.Clients;
using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Middlewares;
using ItemStore.WebApi.csproj.Repositories;
using ItemStore.WebApi.Repositories;
using ItemStore.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using ShopStore.WebApi.csproj.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");

builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(dbConnectionString));
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IShopRepository, ShopRepository>();
builder.Services.AddTransient<IShopService, ShopService>();
builder.Services.AddTransient<JsonPlaceholderClient>();

builder.Services.AddHttpClient<IJsonPlaceholderClient, JsonPlaceholderClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
});

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
