using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Repositories;
using ItemStore.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection");
//builder.Services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(dbConnectionString));

builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(dbConnectionString));
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();

builder.Services.AddAutoMapper(typeof(Program));
//Finish adding automapper

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
