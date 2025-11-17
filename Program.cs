using Enterprise.Controllers;
using Enterpriseservices;
using Gas.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // ✅ Correct namespace
using SecurityTools;
using System;                // <-- this brings Console into scope
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks; // <-- this brings Task into scope
using System.Threading.Tasks;
using SecurityTools;
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
        var connectionString = builder.Configuration.GetConnectionString("GashubConnection");

        builder.Services.AddDbContext<GashubContext>(options =>
            options.UseSqlite(connectionString));
        
        var app = builder.Build();
        app.UseMiddleware<SwaggerAuthMiddleware>();

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
        app.MapFilesProcessedEndpoints();

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
                    Console.WriteLine("  unitapi  - APILogger UnitTest");
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

                case "unitapi":
                    ApiLogHelper.InsertDummyRecord();
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
