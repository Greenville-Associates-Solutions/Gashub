using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
namespace Enterprise.Controllers;
using Gas.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.AspNetCore.Builder;





public static class GasTickerPriceEndpoints
{
    
    public static void MapGasTickerPriceEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/GasTickerPrice").WithTags(nameof(GasTickerPrice));
        Enterpriseservices.Globals.ControllerAPIName = "GasTickerPriceAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.GasTickerPrices.ToList();
            }
            
        })
        .WithName("GetAllGasTickerPrices")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.GasTickerPrices.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetGasTickerPriceById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, GasTickerPrice input) =>
        {
            using (var context = new GashubContext())
            {
                GasTickerPrice[] someGasTickerPrice = context.GasTickerPrices.Where(m => m.Id == id).ToArray();
                context.GasTickerPrices.Attach(someGasTickerPrice[0]);
                if (input.Description != null) someGasTickerPrice[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateGasTickerPrice")
        .WithOpenApi();

        group.MapPost("/", async (GasTickerPrice input) =>
        {
            using (var context = new GashubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.GasTickerPrices.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateGasTickerPrice")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GashubContext())
            {
                //context.GasTickerPrices.Add(std);
                GasTickerPrice[] someGasTickerPrices = context.GasTickerPrices.Where(m => m.Id == id).ToArray();
                context.GasTickerPrices.Attach(someGasTickerPrices[0]);
                context.GasTickerPrices.Remove(someGasTickerPrices[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteGasTickerPrice")
        .WithOpenApi();
    }
}

