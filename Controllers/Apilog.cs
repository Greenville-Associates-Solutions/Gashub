using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;   // for TypedResults
using Microsoft.AspNetCore.OpenApi;    
using System.Linq;
using Microsoft.AspNetCore;
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


//THIS FILE HAS CRUD ENDPOINTS WHICH ARE GENERIC FOR THE Apilog Table. (GET, PUT, POST, DELETE).
//IT IS IDENTICAL TO THE FORM OF THE OTHER CONTROLLERS.

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

       group.MapPut("/api/apilog/{id}", async (int id, Apilog input) =>
        {
        using (var context = new GashubContext())
        {
        Apilog[] someApilog = context.Apilogs.Where(m => m.Id == id).ToArray();
        context.Apilogs.Attach(someApilog[0]);

        if (input.Description != null) someApilog[0].Description = input.Description;
        if (input.Apiname != null) someApilog[0].Apiname = input.Apiname;
        if (input.Eptype != null) someApilog[0].Eptype = input.Eptype;
        if (input.Parameterlist != null) someApilog[0].Parameterlist = input.Parameterlist;
        if (input.Apiresult != null) someApilog[0].Apiresult = input.Apiresult;
        if (input.Apinumber != null) someApilog[0].Apinumber = input.Apinumber;

        // numeric field can be updated directly
        someApilog[0].Hashid = input.Hashid;

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

