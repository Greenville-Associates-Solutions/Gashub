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
           

            using (var context = new GashubContext())
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
            using (var context = new GashubContext())
            {
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "GETWITHID", 1, "Test", "Test"); 
                return context.Gashubs.Where(m => m.Id == id).ToList();
            }
        })
        .WithName("GetGashubById")
        .WithOpenApi();

        //[HttpPut]
        group.MapPut("/{id}", async (int id, Gashub input) =>
        {
            using (var context = new GashubContext())
    {
        Gashub[] someGashub = context.Gashubs.Where(m => m.Id == id).ToArray();
        context.Gashubs.Attach(someGashub[0]);

        if (input.Description != null) someGashub[0].Description = input.Description;
        if (input.CompanyId != null) someGashub[0].CompanyId = input.CompanyId;
        if (input.CompanyName != null) someGashub[0].CompanyName = input.CompanyName;
        if (input.MailAddress1 != null) someGashub[0].MailAddress1 = input.MailAddress1;
        if (input.MailAddress2 != null) someGashub[0].MailAddress2 = input.MailAddress2;
        if (input.MailCity != null) someGashub[0].MailCity = input.MailCity;
        if (input.MailState != null) someGashub[0].MailState = input.MailState;
        if (input.MailZip != null) someGashub[0].MailZip = input.MailZip;
        if (input.MailCountry != null) someGashub[0].MailCountry = input.MailCountry;
        if (input.ShipAddress1 != null) someGashub[0].ShipAddress1 = input.ShipAddress1;
        if (input.ShipAddress2 != null) someGashub[0].ShipAddress2 = input.ShipAddress2;
        if (input.ShipCity != null) someGashub[0].ShipCity = input.ShipCity;
        if (input.ShipState != null) someGashub[0].ShipState = input.ShipState;
        if (input.ShipZip != null) someGashub[0].ShipZip = input.ShipZip;
        if (input.ShipCountry != null) someGashub[0].ShipCountry = input.ShipCountry;
        if (input.Glaccount != null) someGashub[0].Glaccount = input.Glaccount;
        if (input.SubAccount != null) someGashub[0].SubAccount = input.SubAccount;
        if (input.CompanyPhone != null) someGashub[0].CompanyPhone = input.CompanyPhone;
        if (input.CompanyFax != null) someGashub[0].CompanyFax = input.CompanyFax;
        if (input.CompanyEmail != null) someGashub[0].CompanyEmail = input.CompanyEmail;
        if (input.CompanyCare != null) someGashub[0].CompanyCare = input.CompanyCare;
        if (input.GasTicker != null) someGashub[0].GasTicker = input.GasTicker;

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
        .WithName("UpdateGashub")
        .WithOpenApi();

        group.MapPost("/", async (Gashub input) =>
        {
            using (var context = new GashubContext())
            {
                Random rnd = new Random();
                int dice = rnd.Next(1000, 10000000);
                //input.Id = dice;
                context.Gashubs.Add(input);
                await context.SaveChangesAsync();
                Enterpriseservices.ApiLogger.logapi(Enterpriseservices.Globals.ControllerAPIName, Enterpriseservices.Globals.ControllerAPINumber, "NEWRECORD", 1, "TEST", "TEST");
                return TypedResults.Created("Created ID:" + input.Id);
            }

        })
        .WithName("CreateGashub")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id) =>
        {
            using (var context = new GashubContext())
            {
                //context.Gashubs.Add(std);
                Gashub[] someGashubs = context.Gashubs.Where(m => m.Id == id).ToArray();
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
