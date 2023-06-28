using API.AirBnbInsights.Models;
using API.AirBnbInsights.Repositories;
using API.AirBnbInsights.Repositories.Interfaces;
using API.AirBnbInsights.Services;
using API.AirBnbInsights.Services.Interfaces;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

var AllowSpecificOrigins = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Modify CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://polite-pebble-0f8a1d003.3.azurestaticapps.net")
            .AllowAnyHeader()
            .WithMethods("GET")
            .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InsightsDbContext>();
builder.Services.AddScoped<IListingRepository ,ListingRepository>();
builder.Services.AddScoped<IChartsService, ChartsService>();

builder.Services.AddFusionCache()
    .WithSerializer(
        new FusionCacheNewtonsoftJsonSerializer()
    )
    .WithDistributedCache(
        new RedisCache(new RedisCacheOptions { Configuration = builder.Configuration.GetConnectionString("Redis") })
    )
    .WithDefaultEntryOptions(new FusionCacheEntryOptions
    {
        Duration = TimeSpan.FromMinutes(2),
        IsFailSafeEnabled = true,
        FailSafeMaxDuration = TimeSpan.FromHours(2),
        FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
        FactoryHardTimeout = TimeSpan.FromMinutes(2)
    });

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "deny");
    context.Response.Headers.Add("Content-Security-Policy", "default-src https: data: blob: 'unsafe-inline' 'self'");
    context.Response.Cookies.Append("cookieName", "cookieValue", new CookieOptions
    {
        SameSite = SameSiteMode.None,
        Secure = !app.Environment.IsDevelopment(),
    });
    context.Response.Headers.Remove("X-Powered-By");
    await next.Invoke();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

