using Gas.Models;
using Microsoft.OpenApi.Models; // ✅ Correct namespace
using SecurityTools;
using System;                // <-- this brings Console into scope
using System.Threading.Tasks; // <-- this brings Task into scope
using System.Text;
using System.Text.Json;
using Enterprise.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Linq;
namespace GasPriceAnalysis;
class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GasAPI", Version = "v1" });
        });

        var app = builder.Build();

        // Middleware pipeline
        app.UseCors("UnifiedCors");
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();

        // Custom endpoints
        app.MapGashubEndpoints();
        app.MapGasPriceRecordEndpoints();
        app.MapApilogEndpoints();
        app.MapGasTickerPriceEndpoints();

        // Run REST API in background
        var webTask = app.RunAsync();

        // Start CLI loop
        await RunCliAsync();

        // When CLI exits, stop the web server
        await app.StopAsync();
    }

    static async Task RunCliAsync()
    {
        Console.WriteLine("=== Gas Price CLI ===");
        Console.WriteLine("Type 'help' for commands, 'exit' to quit.");

        string? input;
        do
        {
            Console.Write("> ");
            input = Console.ReadLine();

            switch (input?.ToLower())
            {
                case "help":
                    Console.WriteLine("Available commands:");
                    Console.WriteLine("  hubs   - Show all gas hubs");
                    Console.WriteLine("  status - Show API status");
                    Console.WriteLine("  prices - Show Prices Per Day");
                    Console.WriteLine("  exit   - Quit CLI");
                    break;

                case "hubs":
                    using (var context = new GashubContext())
                    {
                            var hubs = context.Gashubs
                            .Select(g => new { g.Id, g.GasTicker })
                            .ToList();

                        Console.WriteLine("Gas Hubs:");
                        foreach (var hub in hubs)
                            Console.WriteLine($"- {hub}");
                    }
                    break;


                    case "prices":
                    using (var context = new GashubContext())
                    {
                    var firstFiveDays = context.GasPriceRecords
                    .OrderBy(r => r.RecordDate)
                    .Take(5)
                    .ToList();

                    foreach (var rec in firstFiveDays)
                    {
                    Console.WriteLine($"Date: {rec.RecordDate:yyyy-MM-dd}");

                    if (!string.IsNullOrEmpty(rec.Ticker1) && rec.Price1.HasValue)
                    Console.WriteLine($"  {rec.Ticker1}: {rec.Price1}");

                    if (!string.IsNullOrEmpty(rec.Ticker2) && rec.Price2.HasValue)
                    Console.WriteLine($"  {rec.Ticker2}: {rec.Price2}");

                    if (!string.IsNullOrEmpty(rec.Ticker3) && rec.Price3.HasValue)
                    Console.WriteLine($"  {rec.Ticker3}: {rec.Price3}");

                    if (!string.IsNullOrEmpty(rec.Ticker4) && rec.Price4.HasValue)
                    Console.WriteLine($"  {rec.Ticker4}: {rec.Price4}");

                    if (!string.IsNullOrEmpty(rec.Ticker5) && rec.Price5.HasValue)
                    Console.WriteLine($"  {rec.Ticker5}: {rec.Price5}");
                    }
                    }
                    break;


                case "status":
                    Console.WriteLine("API is running at http://localhost:59502, https://localhost:59503");
                    break;

                case "exit":
                    Console.WriteLine("Exiting CLI...");
                    break;

                default:
                    if (!string.IsNullOrWhiteSpace(input))
                        Console.WriteLine($"Unknown command: {input}");
                    break;
            }

        } while (input?.ToLower() != "exit");

        await Task.CompletedTask;
    }
}