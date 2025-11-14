using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class LinuxConsoleController : ControllerBase
{
    private readonly string[] databaseNodes = { "10.144.0.100", "10.144.1.100", "10.144.2.100", "10.144.3.100", "10.144.4.100" };
    private readonly string[] webServerNodes = { "10.144.0.152", "10.144.1.151", "10.144.2.151", "10.144.3.151", "10.144.4.151" };
    private readonly string[] gashubNodes = { "10.144.0.152", "10.144.1.151", "10.144.2.151", "10.144.3.151", "10.144.4.151" };

    [HttpGet("{option}")]
    public IActionResult GetSystemInfo(int option)
    {
        switch (option)
        {
            case 1: return ListGasHubs(); // Linux System Information
            case 2: return GetPriceHistory(1);//("/bin/bash", "-c \"netstat -ntla\""); // Network Status
            case 3: return GetServerIPs(); // Get IPs
            case 4: return PingServers(); // Ping IPs with 3 attempts and 2s timeout
            case 5: return ExecuteCommand("/bin/bash", "-c \"ifconfig"); // Return IP Information
            case 99: return RunMigrations(); // Run Database Migrations
            default:
                return BadRequest(new { Error = "Invalid option. Use 1 for system info, 2 for network status, 3 for IPs, 4 for latency, or 99 for migrations." });
        }
    }

    private IActionResult ExecuteCommand(string command, string arguments)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command} {arguments}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return Ok(new { Command = $"{command} {arguments}", Output = output.Trim() });
    }

    private IActionResult GetServerIPs()
    {
        var response = new
        {
            DatabaseNodes = databaseNodes,
            WebServerNodes = webServerNodes
        };

        return Ok(response);
    }

    private IActionResult PingServers()
    {
        var results = new
        {
            DatabaseLatency = databaseNodes.Select(ip => new { IP = ip, Latency = GetPingLatency(ip) }),
            WebServerLatency = webServerNodes.Select(ip => new { IP = ip, Latency = GetPingLatency(ip) })
        };

        return Ok(results);
    }

    private string GetPingLatency(string ip)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"ping -c 3 -W 2 {ip}\"", // 3 pings with 2s timeout
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = processStartInfo };

            if (!process.Start())
            {
                return "DNR"; // Did Not Respond
            }

            bool exited = process.WaitForExit(7000); // Ensure process exits within 7 seconds

            if (!exited)
            {
                process.Kill(); // Kill process if stuck
                return "DNR";
            }

            string output = process.StandardOutput.ReadToEnd();

            if (output.Contains("Destination Host Unreachable") || output.Contains("ping:"))
            {
                return "DNR";
            }

            var latencyLines = output.Split('\n')
                .Where(line => line.Contains("avg"))
                .Select(line => line.Split('/')[4].Trim() + "ms")
                .FirstOrDefault();

            return latencyLines ?? "Latency unavailable";
        }
        catch
        {
            return "DNR";
        }
    }
    private IActionResult ListGasHubs()
    {
        var response = new
        {
            OurGasHubs = gashubNodes
         };

        return Ok(response);
    }

    private IActionResult GetPriceHistory(int a)
    {
        var response = new
        {
            OurGasHubs = gashubNodes
        };

        return Ok(response);
    }




    private IActionResult RunMigrations()
    {
        try
        {
            string[] migrationCommands =
            {
                "/usr/bin/dotnet ef migrations add Initial",
                "/usr/bin/dotnet ef database update",
                "/usr/bin/dotnet ef migrations add SchemaChanges",
                "/usr/bin/dotnet ef database update"
            };

            foreach (var command in migrationCommands)
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = new Process { StartInfo = processStartInfo };
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(new { Error = error.Trim() });
                }
            }

            return Ok(new { Message = "Migrations applied successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = $"Migration failed: {ex.Message}" });
        }
    }
}

