using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using GasHub.Models;
using GasHub.Data;
using Microsoft.AspNetCore.Routing;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
namespace Enterprise.Controllers;


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
           

            using (var context = new GasHubContext())
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
            using (var context = new GasHubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.GasPriceRecords.Where(m => m.GasPriceRecordId == id).ToList();
            }
        })
        .WithName("GetGasPriceRecordById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, GasPriceRecord input) =>
        {
            using (var context = new GasHubContext())
            {
                GasPriceRecord[] someGasPriceRecord = context.GasPriceRecords.Where(m => m.GasPriceRecordId == id).ToArray();
                context.GasPriceRecords.Attach(someGasPriceRecord[0]);
                if (input.Description != null) someGasPriceRecord[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.GasPriceRecordId);
            }


        })
        .WithName("UpdateGasPriceRecord")
        .WithOpenApi();

        group.MapPost("/", async (GasPriceRecord input) =>
        {
            using (var context = new GasHubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.GasPriceRecords.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.GasPriceRecordId);
            }

        })
        .WithName("CreateGasPriceRecord")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GasHubContext())
            {
                //context.GasPriceRecords.Add(std);
                GasPriceRecord[] someGasPriceRecords = context.GasPriceRecords.Where(m => m.GasPriceRecordId == id).ToArray();
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

