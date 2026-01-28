using Microsoft.EntityFrameworkCore;
using FamilyFinance.Api.Data;
using FamilyFinance.Api.Core.Interfaces;
using FamilyFinance.Api.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// 1. Configuração da Connection String
// --------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// --------------------------------------------------
// 2. Registro do DbContext com PostgreSQL
// --------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// --------------------------------------------------
// 3. Registro dos Services para Dependency Injection
// --------------------------------------------------
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBudgetsService, BudgetsService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// --------------------------------------------------
// 4. Controllers e JSON Options
// --------------------------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
// IgnoreCycles evita problemas de referência circular (Family → Members → Family)

// --------------------------------------------------
// 5. Swagger/OpenAPI
// --------------------------------------------------
builder.Services.AddEndpointsApiExplorer(); // Habilita exploração de endpoints
builder.Services.AddSwaggerGen();           // Gera a documentação OpenAPI

// --------------------------------------------------
// 6. CORS (Cross-Origin Resource Sharing)
// --------------------------------------------------
builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowAll", builder =>
   {
      builder.AllowAnyOrigin()   // Permite qualquer domínio
              .AllowAnyMethod()   // Permite GET, POST, PUT, DELETE
              .AllowAnyHeader();  // Permite qualquer header
   });
});

var app = builder.Build();

// --------------------------------------------------
// 7. Middlewares
// --------------------------------------------------
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();     // Ativa Swagger JSON
   app.UseSwaggerUI();   // Ativa a interface web do Swagger
}

app.UseHttpsRedirection(); // Força HTTPS
app.UseCors("AllowAll");   // Aplica a política de CORS
app.UseAuthorization();    // Middleware de autorização (JWT ou policies)

// Mapeia todos os Controllers para endpoints
app.MapControllers();

// Log de inicialização
app.Logger.LogInformation("API de Finanças Familiares iniciou com sucesso!");

// Inicia a aplicação
app.Run();
