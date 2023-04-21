using AirBnbInsights;
using AirBnbInsights.Database;
using Microsoft.OpenApi.Models;
using ZiggyCreatures.Caching.Fusion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterModules();
builder.Services.AddDbContext<AirBnbContext>();

// CONNECTION STRINGS
var sqlConn = builder.Configuration.GetConnectionString("Sql");
var redisConn = builder.Configuration.GetConnectionString("Redis");

if (string.IsNullOrWhiteSpace(sqlConn))
    throw new NullReferenceException("You must specify a sql connection (see appsettings.json)");

// ADD SERVICES: REDIS
if (string.IsNullOrWhiteSpace(redisConn) == false)
{
    // ADD SERVICES: REDIS DISTRIBUTED CACHE
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConn;
    });

    // ADD SERVICES: JSON SERIALIZER
    builder.Services.AddFusionCacheSystemTextJsonSerializer();

    // ADD SERVICES: REDIS BACKPLANE
    builder.Services.AddFusionCacheStackExchangeRedisBackplane(options =>
    {
        options.Configuration = redisConn;
    });
}

// ADD SERVICES: FUSIONCACHE
builder.Services.AddFusionCache().TryWithAutoSetup();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "",
        Description = "",
        Version = "v1"
    });
});

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
