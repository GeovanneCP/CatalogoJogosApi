using CatalogoJogosApi.Configurations;
using CatalogoJogosApi.Repositories;
using CatalogoJogosApi.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o Options Pattern para o MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings")
);

var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
var connectionString = mongoSettings?.ConnectionString ?? "mongodb://localhost:27017";

// 2. Registar o IMongoClient como Singleton
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));

// 3. Injeção de Dependência das Camadas (DIP - SOLID)
builder.Services.AddScoped<IJogoRepository, JogoRepository>();
builder.Services.AddScoped<IJogoService, JogoService>();

// 4. Adicionar Health Check para monitorização do MongoDB
builder.Services.AddHealthChecks()
    .AddAsyncCheck("mongodb", async () =>
    {
        try
        {
            var client = new MongoClient(connectionString);
            await client.ListDatabaseNamesAsync();
            return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy(ex.Message);
        }
    }, tags: new[] { "db", "data" });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 5. Mapear o endpoint de Health Check
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();