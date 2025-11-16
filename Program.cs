using Enterprise.Controllers;
using Gas.Models;
using Microsoft.OpenApi.Models; // ✅ Correct namespace
using SecurityTools;
using System.Text;
using System.Text.Json;
using Enterprise.Controllers;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Load CORS settings from appsettings.json
var corsSettings = builder.Configuration.GetSection("Cors");
//var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
//var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>() ?? Array.Empty<string>();
//var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>() ?? Array.Empty<string>();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GasAPI", Version = "v1" });
});

// Register CORS policy
//builder.Services.AddCors(options =>
//{
  //  options.AddPolicy("UnifiedCors", policy =>
    //{
    //    policy.SetIsOriginAllowed(origin =>
    //        string.IsNullOrEmpty(origin) || origin == "null" || allowedOrigins.Contains(origin))
    //        .WithMethods(allowedMethods)
    //        .WithHeaders(allowedHeaders);
    //});
//});

var app = builder.Build();

// Custom middleware (must exist in your project)
//app.UseMiddleware<SwaggerAuthMiddleware>();

// Middleware pipeline
app.UseCors("UnifiedCors");
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers(); // ✅ ensures controllers are mapped

// Custom endpoints (must exist as extension methods)
app.MapGashubEndpoints();
app.MapGasPriceRecordEndpoints();
app.MapApilogEndpoints();
app.MapGasTickerPriceEndpoints();
app.Run();
