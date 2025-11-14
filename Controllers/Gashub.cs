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
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.AspNetCore.Routing;
namespace Enterprise.Controllers;


public static class GashubEndpoints
{
    
    public static void MapGashubEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Gashub").WithTags(nameof(Gashub));
        Enterpriseservices.Globals.ControllerAPIName = "GashubAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new GasHubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Gashubs.ToList();
            }
            
        })
        .WithName("GetAllGashubs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new GasHubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Gashubs.Where(m => m.GashubId == id).ToList();
            }
        })
        .WithName("GetGashubById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Gashub input) =>
        {
            using (var context = new GasHubContext())
            {
                Gashub[] someGashub = context.Gashubs.Where(m => m.GashubId == id).ToArray();
                context.Gashubs.Attach(someGashub[0]);
                if (input.Description != null) someGashub[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.GashubId);
            }


        })
        .WithName("UpdateGashub")
        .WithOpenApi();

        group.MapPost("/", async (Gashub input) =>
        {
            using (var context = new GasHubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Gashubs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.GashubId);
            }

        })
        .WithName("CreateGashub")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GasHubContext())
            {
                //context.Gashubs.Add(std);
                Gashub[] someGashubs = context.Gashubs.Where(m => m.GashubId == id).ToArray();
                context.Gashubs.Attach(someGashubs[0]);
                context.Gashubs.Remove(someGashubs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteGashub")
        .WithOpenApi();
    }
}

