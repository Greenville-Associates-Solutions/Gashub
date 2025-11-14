using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Gas.Models;
using Enterpriseservices;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
namespace Enterprise.Controllers;


public static class ApilogEndpoints
{
    
    public static void MapApilogEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Apilog").WithTags(nameof(Apilog));
        Enterpriseservices.Globals.ControllerAPIName = "ApilogAPI";
        Enterpriseservices.Globals.ControllerAPINumber = "001";
        
        //[HttpGet]
        group.MapGet("/", () =>
        {
           

            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GET", 1, "Test", "Test");
                return context.Apilogs.ToList();
            }
            
        })
        .WithName("GetAllApilogs")
        .WithOpenApi();

        //[HttpGet]
        group.MapGet("/{id}", (int id) =>
        {
            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Apilogs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetApilogById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Apilog input) =>
        {
            using (var context = new GashubContext())
            {
                Apilog[] someApilog = context.Apilogs.Where(m => m.Id == id).ToArray();
                context.Apilogs.Attach(someApilog[0]);
                if (input.Description != null) someApilog[0].Description = input.Description;
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "PUTWITHID", 1, "Test", "Test");
                return TypedResults.Accepted("Updated ID:" + input.Id);
            }


        })
        .WithName("UpdateApilog")
        .WithOpenApi();

        group.MapPost("/", async (Apilog input) =>
        {
            using (var context = new GashubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Apilogs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateApilog")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GashubContext())
            {
                //context.Apilogs.Add(std);
                Apilog[] someApilogs = context.Apilogs.Where(m => m.Id == id).ToArray();
                context.Apilogs.Attach(someApilogs[0]);
                context.Apilogs.Remove(someApilogs[0]);
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "DELETEWITHID",1, "TEST", "TEST");
                await context.SaveChangesAsync();
            }

        })
        .WithName("DeleteApilog")
        .WithOpenApi();
    }
}

