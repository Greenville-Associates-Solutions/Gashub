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

//THIS FILE HAS CRUD ENDPOINTS WHICH ARE GENERIC FOR THE FilesProcessed Table. (GET, PUT, POST, DELETE).
//IT IS IDENTICAL TO THE FORM OF THE OTHER CONTROLLERS.

namespace Enterprise.Controllers
{
    public static class FilesProcessedEndpoints
    {
        public static void MapFilesProcessedEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/FilesProcessed").WithTags(nameof(FilesProcessed));
            Enterpriseservices.Globals.ControllerAPIName = "FilesProcessedAPI";
            Enterpriseservices.Globals.ControllerAPINumber = "002";

            // [HttpGet] - Get all
            group.MapGet("/", () =>
            {
                using (var context = new GashubContext())
                {
                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "GET",
                        1,
                        "Test",
                        "Test"
                    );
                    return context.FilesProcessed.ToList();
                }
            })
            .WithName("GetAllFilesProcessed")
            .WithOpenApi();

            // [HttpGet] - Get by ID
            group.MapGet("/{id}", (int id) =>
            {
                using (var context = new GashubContext())
                {
                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "GETWITHID",
                        1,
                        "Test",
                        "Test"
                    );
                    return context.FilesProcessed.Where(m => m.Id == id).ToList();
                }
            })
            .WithName("GetFilesProcessedById")
            .WithOpenApi();

            // [HttpPut] - Update by ID
            group.MapPut("/{id}", async (int id, FilesProcessed input) =>
            {
                using (var context = new GashubContext())
                {
                    FilesProcessed[] someFiles = context.FilesProcessed.Where(m => m.Id == id).ToArray();
                    context.FilesProcessed.Attach(someFiles[0]);

                    if (input.FilePath != null) someFiles[0].FilePath = input.FilePath;
                    if (input.FileDate != null) someFiles[0].FileDate = input.FileDate;
                    if (input.ProcessedDateTime != null) someFiles[0].ProcessedDateTime = input.ProcessedDateTime;

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
            .WithName("UpdateFilesProcessed")
            .WithOpenApi();

            // [HttpPost] - Create new
            group.MapPost("/", async (FilesProcessed input) =>
            {
                using (var context = new GashubContext())
                {
                    context.FilesProcessed.Add(input);
                    await context.SaveChangesAsync();

                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "NEWRECORD",
                        1,
                        "TEST",
                        "TEST"
                    );

                    return TypedResults.Created("Created ID:" + input.Id);
                }
            })
            .WithName("CreateFilesProcessed")
            .WithOpenApi();

            // [HttpDelete] - Delete by ID
            group.MapDelete("/{id}", async (int id) =>
            {
                using (var context = new GashubContext())
                {
                    FilesProcessed[] someFiles = context.FilesProcessed.Where(m => m.Id == id).ToArray();
                    context.FilesProcessed.Attach(someFiles[0]);
                    context.FilesProcessed.Remove(someFiles[0]);

                    Enterpriseservices.ApiLogger.logapi(
                        Enterpriseservices.Globals.ControllerAPIName,
                        Enterpriseservices.Globals.ControllerAPINumber,
                        "DELETEWITHID",
                        1,
                        "TEST",
                        "TEST"
                    );

                    await context.SaveChangesAsync();
                }
            })
            .WithName("DeleteFilesProcessed")
            .WithOpenApi();
        }
    }
}
