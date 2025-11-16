using Microsoft.AspNetCore.Mvc;
using System;
using Enterpriseservices; // <-- bring in FileIO

[ApiController]
[Route("[controller]")]
public class GasActionsController : ControllerBase
{
    private readonly string[] databaseNodes = { "10.144.0.100", "10.144.1.100", "10.144.2.100", "10.144.3.100", "10.144.4.100" };
    private readonly string[] webServerNodes = { "10.144.0.152", "10.144.1.151", "10.144.2.151", "10.144.3.151", "10.144.4.151" };
    private readonly string[] gashubNodes = { "Tulsa", "Austin", "Dallas", "OklahomaCity", "Phoenix" };

    [HttpGet("{option}")]
    public IActionResult GetSystemInfo(int option)
    {
        switch (option)
        {
            case 1: return ListGasHubs();
            case 2: return GetPriceHistory();
            case 3: return ProcessPrices();
            case 4: return GetFileList();   
            case 5: return RunFullPipeline(); 
            case 6: return RunPipeline_1_2_4();
            case 7: return RunPipeline_1_2_5();
            default:
                return BadRequest(new { Error = "Invalid option. Use 1 for hubs, 2 for history, 3 for branches, 4 for file list, 5 for full pipeline, 6 for 1-2-4, 7 for 1-2-5." });
        }
    }

    // -------------------------------
    // OPTION #4
    // -------------------------------
    private IActionResult GetFileList()
    {
        try
        {
            var result = FileIO.GetDataFiles();
            return Ok(new
            {
                FileCount = result.FileCount,
                Files = result.FileNames
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = $"Failed to get file list: {ex.Message}" });
        }
    }

    private IActionResult ListGasHubs()
    {
        return Ok(new { OurGasHubs = gashubNodes });
    }

    private IActionResult GetPriceHistory()
    {
        return Ok(new { OurGasHubs = gashubNodes });
    }

    private IActionResult ProcessPrices()
    {
        return Ok(new { Branches = gashubNodes });
    }

    // -------------------------------
    // OPTION #5: Full Pipeline
    // -------------------------------
    private IActionResult RunFullPipeline()
    {
        try
        {
            var files = FileIO.GetDataFiles();
            string jsonOutput = FileIO.ProcessCsvFilesToJson();
            FileIO.PostJsonToDatabase(jsonOutput);
            FileIO.InsertNewTickersIntoGashub(jsonOutput);
            FileIO.PopulateGasTickerPrice(jsonOutput);

            return Ok(new
            {
                Message = "Pipeline 5 executed successfully.",
                FileCount = files.FileCount,
                Files = files.FileNames
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = $"Error(5): Pipeline failed + {ex.Message}" });
        }
    }

    // -------------------------------
    // OPTION #6: Functions 1,2,4
    // -------------------------------
    private IActionResult RunPipeline_1_2_4()
    {
        try
        {
            var files = FileIO.GetDataFiles();
            var jsonOutput = FileIO.ProcessCsvFilesToJson();
            FileIO.InsertNewTickersIntoGashub(jsonOutput);

            return Ok(new
            {
                Message = "Pipeline 6 executed successfully (Functions 1,2,4).",
                FileCount = files.FileCount,
                Files = files.FileNames
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = $"Error(6): Pipeline failed + {ex.Message}" });
        }
    }

    // -------------------------------
    // OPTION #7: Functions 1,2,5
    // -------------------------------
    private IActionResult RunPipeline_1_2_5()
    {
        try
        {
            var files = FileIO.GetDataFiles();
            var jsonOutput = FileIO.ProcessCsvFilesToJson();
            FileIO.PopulateGasTickerPrice(jsonOutput);

            return Ok(new
            {
                Message = "Pipeline 7 executed successfully (Functions 1,2,5).",
                FileCount = files.FileCount,
                Files = files.FileNames
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = $"Error(7): Pipeline failed + {ex.Message}" });
        }
    }
}




