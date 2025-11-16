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





public static class GasPriceRecordEndpoints
{
    
    public static void MapGasPriceRecordEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/GasPriceRecord").WithTags(nameof(GasPriceRecord));
        Enterpriseservices.Globals.ControllerAPIName = "GasPriceRecordAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.GasPriceRecords.ToList();
            }
            
        })
        .WithName("GetAllGasPriceRecords")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.GasPriceRecords.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetGasPriceRecordById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, GasPriceRecord input) =>
        {
           using (var context = new GashubContext())
    {
        GasPriceRecord[] someGasPriceRecord = context.GasPriceRecords.Where(m => m.Id == id).ToArray();
        context.GasPriceRecords.Attach(someGasPriceRecord[0]);

        if (input.Description != null) someGasPriceRecord[0].Description = input.Description;

        // ✅ Tickers (strings only if not null)
        if (input.Ticker1 != null) someGasPriceRecord[0].Ticker1 = input.Ticker1;
        if (input.Ticker2 != null) someGasPriceRecord[0].Ticker2 = input.Ticker2;
        if (input.Ticker3 != null) someGasPriceRecord[0].Ticker3 = input.Ticker3;
        if (input.Ticker4 != null) someGasPriceRecord[0].Ticker4 = input.Ticker4;
        if (input.Ticker5 != null) someGasPriceRecord[0].Ticker5 = input.Ticker5;
        if (input.Ticker6 != null) someGasPriceRecord[0].Ticker6 = input.Ticker6;
        if (input.Ticker7 != null) someGasPriceRecord[0].Ticker7 = input.Ticker7;
        if (input.Ticker8 != null) someGasPriceRecord[0].Ticker8 = input.Ticker8;
        if (input.Ticker9 != null) someGasPriceRecord[0].Ticker9 = input.Ticker9;
        if (input.Ticker10 != null) someGasPriceRecord[0].Ticker10 = input.Ticker10;

        // ✅ Prices (numeric, overwrite directly)
        someGasPriceRecord[0].Price1 = input.Price1;
        someGasPriceRecord[0].Price2 = input.Price2;
        someGasPriceRecord[0].Price3 = input.Price3;
        someGasPriceRecord[0].Price4 = input.Price4;
        someGasPriceRecord[0].Price5 = input.Price5;
        someGasPriceRecord[0].Price6 = input.Price6;
        someGasPriceRecord[0].Price7 = input.Price7;
        someGasPriceRecord[0].Price8 = input.Price8;
        someGasPriceRecord[0].Price9 = input.Price9;
        someGasPriceRecord[0].Price10 = input.Price10;

        // ✅ Other fields
        someGasPriceRecord[0].RecordDate = input.RecordDate;
        someGasPriceRecord[0].DailyAverage = input.DailyAverage;
        someGasPriceRecord[0].TickerTotals = input.TickerTotals;

        await context.SaveChangesAsync();

        Enterpriseservices.ApiLogger.logapi(
            Enterpriseservices.Globals.ControllerAPIName,
            Enterpriseservices.Globals.ControllerAPINumber,
            "PUTWITHID",
            1,
            "Test",
            "Test"
        );

        return TypedResults.Accepted("Updated ID:" + input.Id);
    }


        })
        .WithName("UpdateGasPriceRecord")
        .WithOpenApi();

        group.MapPost("/", async (GasPriceRecord input) =>
        {
            using (var context = new GashubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.GasPriceRecords.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateGasPriceRecord")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GashubContext())
            {
                //context.GasPriceRecords.Add(std);
                GasPriceRecord[] someGasPriceRecords = context.GasPriceRecords.Where(m => m.Id == id).ToArray();
                context.GasPriceRecords.Attach(someGasPriceRecords[0]);
                context.GasPriceRecords.Remove(someGasPriceRecords[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteGasPriceRecord")
        .WithOpenApi();
    }
}

