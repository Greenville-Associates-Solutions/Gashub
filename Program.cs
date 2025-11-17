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
using Enterpriseservices;
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

// 🔧 Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("UnifiedCors", policy =>
            {
                policy.AllowAnyOrigin()   // or restrict to WithOrigins("http://localhost:8080")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
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

      Console.WriteLine("Initializing database...");
      Console.WriteLine("Reading data files...");
      Console.WriteLine("Data processing complete!");
      Console.WriteLine("Reloading data can be done from the CLI Options Menu!");



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
                    Console.WriteLine("  status   - Show API status");
                    Console.WriteLine("  hubs     - Show all gas hubs");
                    Console.WriteLine("  prices   - Show Prices Per Day");
                    Console.WriteLine("  priceday - Show Prices For One Day");
                    Console.WriteLine("  tickhist - Show Ticker Prices Per Day");
                    Console.WriteLine("  initall  - Update DB from PriceData");
                    Console.WriteLine("  erasedb  - Delete All Existing Records - Note Doesnt Reset AutoIncrement Ids in SQLITE");
                    Console.WriteLine("  exit   - Quit CLI");
                    break;

                case "hubs":
                    CLISupport.ShowHubs();
                break;

                case "prices":
                   CLISupport.ShowPrices();
                 break;

                case "status":
                    Console.WriteLine("API is running at http://localhost:59502, https://localhost:59503");
                    break;

                case "tickhist":
                    CLISupport.PromptAndShowTickerPrices();
                    break;

                case "priceday":
                    CLISupport.PromptAndShowPricesByDate();
                    break;

                case "initall":
                    CLISupport.RunFullPipeline();
                    break;
                
                case "erasedb":
                    CLISupport.EraseDB();
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
