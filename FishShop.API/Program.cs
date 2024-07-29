using FishShop.API;
using FishShop.API.Cors;
using FishShop.API.Versions;
using FishShop.Core;
using FishShop.Core.Services;
using FishShop.DAL;
using FishShop.GraphQL;
using FishShop.GraphQL.Queries;
using FishShop.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder.Services.AddSwaggerCustom();
builder.Services.AddCustomVersioning();

builder.Services.AddCore();
builder.Services.AddDAL();
builder.Services.AddRabbitMQ();
builder.Services.AddCustomAuth(builder.Configuration);
builder.Services.AddBindOptions(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddMyGraphQl();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
var migrator = scope.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync().ConfigureAwait(false);
await seeder.SeedAsync(CancellationToken.None).ConfigureAwait(true);


app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddExceptionMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.MapMyGraphQl();

app.Run();
