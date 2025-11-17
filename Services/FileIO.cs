using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Gas.Models;
using Microsoft.EntityFrameworkCore;

namespace Enterpriseservices
{
    public static class FileIO
    {
        // -------------------------------
        // Function 1: GetDataFiles
        // -------------------------------
        public static (int FileCount, List<string> FileNames) GetDataFiles()
        {
            try
            {
                string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATA");

                if (!Directory.Exists(dataPath))
                    throw new DirectoryNotFoundException($"Error(1): Function 1 failed — DATA directory not found at {dataPath}");

                var files = Directory.GetFiles(dataPath, "*.csv").ToList();
                return (files.Count, files.Select(Path.GetFileName).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error(1): Function 1 + {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }


        // -------------------------------
        // Function 2: Process CSV Files into JSON Format
        // -------------------------------

 public static string ProcessCsvFilesToJson()
{
    try
    {
        string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATA");
        var files = Directory.GetFiles(dataPath, "*.csv");

        var records = new List<GasPriceRecord>();

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file); // e.g. gas_prices_2024_11_01
            DateTime recordDate = DateTime.Now;

            // Try to parse date from filename
            var parts = fileName.Split('_'); // ["gas","prices","2024","11","01"]
            if (parts.Length >= 5 &&
                int.TryParse(parts[2], out int year) &&
                int.TryParse(parts[3], out int month) &&
                int.TryParse(parts[4], out int day))
            {
                recordDate = new DateTime(year, month, day);
            }

            var lines = File.ReadAllLines(file).Skip(1); // skip header

            var record = new GasPriceRecord
            {
                RecordDate = recordDate,
                Description = $"Imported from {Path.GetFileName(file)}",
                GasHubId = fileName
            };

            int tickerIndex = 1;
            var prices = new List<decimal>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var partsLine = line.Split(';');
                string ticker = partsLine.Length > 0 ? partsLine[0].Trim() : null;
                decimal? price = null;

                if (partsLine.Length > 1 && decimal.TryParse(partsLine[1], out var p))
                    price = p;

                if (!string.IsNullOrEmpty(ticker))
                {
                    switch (tickerIndex)
                    {
                        case 1: record.Ticker1 = ticker; record.Price1 = price; break;
                        case 2: record.Ticker2 = ticker; record.Price2 = price; break;
                        case 3: record.Ticker3 = ticker; record.Price3 = price; break;
                        case 4: record.Ticker4 = ticker; record.Price4 = price; break;
                        case 5: record.Ticker5 = ticker; record.Price5 = price; break;
                        case 6: record.Ticker6 = ticker; record.Price6 = price; break;
                        case 7: record.Ticker7 = ticker; record.Price7 = price; break;
                        case 8: record.Ticker8 = ticker; record.Price8 = price; break;
                        case 9: record.Ticker9 = ticker; record.Price9 = price; break;
                        case 10: record.Ticker10 = ticker; record.Price10 = price; break;
                    }
                    tickerIndex++;

                    if (price.HasValue)
                        prices.Add(price.Value);
                }
            }

            // ✅ Calculate new fields
            record.TickerTotals = prices.Count;
            record.DailyAverage = prices.Count > 0 ? prices.Average() : null;

            records.Add(record);
        }

        return JsonSerializer.Serialize(records);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error(2): Function 2 + {ex.InnerException?.Message ?? ex.Message}");
        throw;
    }
}



        // -------------------------------
        // Function 3: PostJsonToDatabase
        // -------------------------------
        public static void PostJsonToDatabase(string jsonOutput)
        {
            try
            {
                using var context = new GashubContext();

                var records = JsonSerializer.Deserialize<List<GasPriceRecord>>(jsonOutput);
                if (records == null || !records.Any())
                    return;

                context.GasPriceRecords.AddRange(records);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error(3): Function 3 + {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }

        // -------------------------------
        // Function 4: InsertNewTickersIntoGashub
        // -------------------------------
        public static void InsertNewTickersIntoGashub(string jsonOutput)
        {
            try
            {
                using var context = new GashubContext();

                var records = JsonSerializer.Deserialize<List<GasPriceRecord>>(jsonOutput);
                if (records == null || !records.Any())
                    return;

                var tickers = new List<string>();
                foreach (var rec in records)
                {
                    if (!string.IsNullOrEmpty(rec.Ticker1)) tickers.Add(rec.Ticker1);
                    if (!string.IsNullOrEmpty(rec.Ticker2)) tickers.Add(rec.Ticker2);
                    if (!string.IsNullOrEmpty(rec.Ticker3)) tickers.Add(rec.Ticker3);
                    if (!string.IsNullOrEmpty(rec.Ticker4)) tickers.Add(rec.Ticker4);
                    if (!string.IsNullOrEmpty(rec.Ticker5)) tickers.Add(rec.Ticker5);
                    if (!string.IsNullOrEmpty(rec.Ticker6)) tickers.Add(rec.Ticker6);
                    if (!string.IsNullOrEmpty(rec.Ticker7)) tickers.Add(rec.Ticker7);
                    if (!string.IsNullOrEmpty(rec.Ticker8)) tickers.Add(rec.Ticker8);
                    if (!string.IsNullOrEmpty(rec.Ticker9)) tickers.Add(rec.Ticker9);
                    if (!string.IsNullOrEmpty(rec.Ticker10)) tickers.Add(rec.Ticker10);
                }

                var distinctTickers = tickers.Distinct().ToList();
                var existingTickers = context.Gashubs.Select(g => g.GasTicker).ToHashSet();

                foreach (var ticker in distinctTickers)
                {
                    if (!existingTickers.Contains(ticker))
                    {
                        var newHub = new Gashub
                        {
                            GasTicker = ticker,
                            CompanyId = "SomeCompany", 
                            Description = "Auto-created from ticker import"
                        };

                        context.Gashubs.Add(newHub);
                    }
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error(4): Function 4 + {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
        }

        // -------------------------------
        // Function 5: PopulateGasTickerPrice
        // -------------------------------
       public static void PopulateGasTickerPrice(string jsonOutput)
{
    try
    {
        using var context = new GashubContext();

        var records = JsonSerializer.Deserialize<List<GasPriceRecord>>(jsonOutput);
        if (records == null || !records.Any())
            return;

        foreach (var rec in records)
        {
            var tickerPrices = new List<GasTickerPrice>();

            void AddTicker(string? ticker, decimal? price)
            {
                if (!string.IsNullOrEmpty(ticker) && price.HasValue)
                {
                    tickerPrices.Add(new GasTickerPrice
                    {
                        GasTicker = ticker,
                        Price = price.Value,
                        RecordDate = rec.RecordDate.Date,
                        Description = rec.Description
                    });
                }
            }

            // Collect tickers for this record only
            AddTicker(rec.Ticker1, rec.Price1);
            AddTicker(rec.Ticker2, rec.Price2);
            AddTicker(rec.Ticker3, rec.Price3);
            AddTicker(rec.Ticker4, rec.Price4);
            AddTicker(rec.Ticker5, rec.Price5);
            AddTicker(rec.Ticker6, rec.Price6);
            AddTicker(rec.Ticker7, rec.Price7);
            AddTicker(rec.Ticker8, rec.Price8);
            AddTicker(rec.Ticker9, rec.Price9);
            AddTicker(rec.Ticker10, rec.Price10);

            // Insert each ticker for this record
            foreach (var tp in tickerPrices)
            {
                Console.WriteLine($"Attempting insert: {tp.GasTicker} {tp.RecordDate}");

                // Check if already exists to avoid UNIQUE constraint error
                var exists = context.GasTickerPrices
                    .Any(x => x.GasTicker == tp.GasTicker && x.RecordDate == tp.RecordDate);

                if (!exists)
                {
                    context.GasTickerPrices.Add(tp);
                    context.SaveChanges(); // commit immediately for this ticker
                }
                else
                {
                    Console.WriteLine($"Skipped duplicate: {tp.GasTicker} {tp.RecordDate}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error(5): Function 5 + {ex.InnerException?.Message ?? ex.Message}");
        throw;
    }
}

// -------------------------------
// Function 6: WriteProcessed
// -------------------------------
    public static void WriteProcessed(string filepath)
    {
    try
    {
        using var context = new GashubContext();

        var fileName = Path.GetFileNameWithoutExtension(filepath);
        DateTime fileDate = DateTime.Now;

        // Try to parse date from filename (e.g. gas_prices_2024_11_01.csv)
        var parts = fileName.Split('_');
        if (parts.Length >= 5 &&
            int.TryParse(parts[2], out int year) &&
            int.TryParse(parts[3], out int month) &&
            int.TryParse(parts[4], out int day))
        {
            fileDate = new DateTime(year, month, day);
        }

        var entry = new FilesProcessed
        {
            FilePath = filepath,          // full path passed in
            FileDate = fileDate,          // extracted from filename
            ProcessedDateTime = DateTime.Now // actual processing time
        };

        context.FilesProcessed.Add(entry);
        context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error(6): Function 6 + {ex.InnerException?.Message ?? ex.Message}");
        throw;
    }
    }

       

    }
}
