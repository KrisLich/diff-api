using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using DiffAPI.Services;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// API versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                 new HeaderApiVersionReader("x-api-version"),
                                                 new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddEndpointsApiExplorer();
// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Add memory cache for caching data
builder.Services.AddMemoryCache();

// Rate limiting
builder.Services.AddRateLimiter(options =>
options.AddFixedWindowLimiter(policyName: "fixed", options =>
{
    options.PermitLimit = 5;    // Number of requests
    options.Window = TimeSpan.FromSeconds(10); // Timeframe in seconds
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;    
    options.QueueLimit = 3;
}));

// Register service with a single instance
builder.Services.AddSingleton<IDiffService, DiffService>();

var app = builder.Build();

// Swagger documentation
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Rate limiting
app.UseRateLimiter();

app.MapControllers();

app.Run();

// Making our class Program public for use in integration tests
public partial class Program { }