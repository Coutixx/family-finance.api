using Microsoft.EntityFrameworkCore;
using FamilyFinance.Api.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// String Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext to user Postgres
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.MapControllers();
app.UseHttpsRedirection();

app.Logger.LogInformation("A API de Financas Familiares iniciou com sucesso!");
app.Run();

