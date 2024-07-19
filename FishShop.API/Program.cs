using FishShop.API;
using FishShop.API.Versions;
using FishShop.Core;
using FishShop.Core.Services;
using FishShop.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomVersioning();
builder.Services.AddCustomAuth(builder.Configuration);
builder.Services.AddCore();
builder.Services.AddDAL();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(builder.Configuration["AppContext:DatabaseConnection"]));

var app = builder.Build();
using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
var migrator = scope.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();
await seeder.SeedAsync(CancellationToken.None);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();