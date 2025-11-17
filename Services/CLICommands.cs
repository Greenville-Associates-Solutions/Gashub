namespace Enterpriseservices;
using Gas.Models;
using System;                // <-- this brings Console into scope
using System.Text;
using System.Text.Json;
using System.Linq;
using Enterpriseservices;
using System.Security.Cryptography.X509Certificates;


//THIS CLASS SUPPORTS ALL THE CLI COMMANDS FROM THE COMMAND LINE TO EFCORE. IT ASSUMES DATA IS ALREADY LOADED INTO THE DATABASE.

public class CLISupport
{
       //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Function #1 - PromptAndShowTickerPrices()
      // Inputs: This Function takes no arguments and does not restrict the output.
      // Actions: This function requests the ticker name you want to track. It then prints the GasTickerPrices to the Screen as a dump which could be large in a big system...but we have only 5/10 days of data.
      // Returns: This Function Returns Nothing But Writes the Output to System Console (Which when the CLI is running the API as well will include diagnostic calls for every API call).
      //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public static void PromptAndShowTickerPrices()
    {
    Console.Write("Enter ticker symbol: ");
    var ticker = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(ticker))
    {
        Console.WriteLine("No ticker entered.");
        return;
    }

    using (var context = new GashubContext())
    {
        var records = context.GasTickerPrices
            .Where(r => r.GasTicker == ticker)
            .OrderBy(r => r.RecordDate)
            .ToList();

        if (!records.Any())
        {
            Console.WriteLine($"No records found for ticker '{ticker}'.");
            return;
        }

        Console.WriteLine($"Records for ticker '{ticker}':");
        foreach (var rec in records)
        {
            Console.WriteLine($"  Date: {rec.RecordDate:yyyy-MM-dd}, Price: {rec.Price}, Description: {rec.Description}");
        }
        }
        }

      //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Function #2 - PromptAndShowTickerPricesByDate()
      // Inputs: This Function takes no arguments and does not restrict the output.
      // Actions: This function prompts the user for a Date Range.
      // Returns: This Function Returns Nothing But Writes the Output to System Console for only the date requested. (Which when the CLI is running the API as well will include diagnostic calls for every API call).
      //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public static void PromptAndShowPricesByDate()
    {
    Console.Write("Enter month (MM): ");
    var monthInput = Console.ReadLine();

    Console.Write("Enter day (DD): ");
    var dayInput = Console.ReadLine();

    Console.Write("Enter year (YYYY): ");
    var yearInput = Console.ReadLine();

    if (!int.TryParse(monthInput, out int month) ||
        !int.TryParse(dayInput, out int day) ||
        !int.TryParse(yearInput, out int year))
    {
        Console.WriteLine("Invalid date input. Please enter numeric values.");
        return;
    }

    DateTime date;
    try
    {
        date = new DateTime(year, month, day);
    }
    catch
    {
        Console.WriteLine("Invalid date combination.");
        return;
    }

    using (var context = new GashubContext())
    {
        var record = context.GasPriceRecords
            .FirstOrDefault(r => r.RecordDate.Date == date.Date);

        if (record == null)
        {
            Console.WriteLine($"No records found for {date:yyyy-MM-dd}");
            return;
        }

        // Print Date and Daily Average in light blue
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Date: {record.RecordDate:yyyy-MM-dd}");
        if (record.DailyAverage.HasValue)
            Console.WriteLine($"  Avg Daily Price: {record.DailyAverage:F2}");
        else
            Console.WriteLine("  Avg Daily Price: N/A");
        Console.ResetColor();

        // Print tickers and prices
        if (!string.IsNullOrEmpty(record.Ticker1) && record.Price1.HasValue)
            Console.WriteLine($"  {record.Ticker1}: {record.Price1}");
        if (!string.IsNullOrEmpty(record.Ticker2) && record.Price2.HasValue)
            Console.WriteLine($"  {record.Ticker2}: {record.Price2}");
        if (!string.IsNullOrEmpty(record.Ticker3) && record.Price3.HasValue)
            Console.WriteLine($"  {record.Ticker3}: {record.Price3}");
        if (!string.IsNullOrEmpty(record.Ticker4) && record.Price4.HasValue)
            Console.WriteLine($"  {record.Ticker4}: {record.Price4}");
        if (!string.IsNullOrEmpty(record.Ticker5) && record.Price5.HasValue)
            Console.WriteLine($"  {record.Ticker5}: {record.Price5}");
        if (!string.IsNullOrEmpty(record.Ticker6) && record.Price6.HasValue)
            Console.WriteLine($"  {record.Ticker6}: {record.Price6}");
        if (!string.IsNullOrEmpty(record.Ticker7) && record.Price7.HasValue)
            Console.WriteLine($"  {record.Ticker7}: {record.Price7}");
        if (!string.IsNullOrEmpty(record.Ticker8) && record.Price8.HasValue)
            Console.WriteLine($"  {record.Ticker8}: {record.Price8}");
        if (!string.IsNullOrEmpty(record.Ticker9) && record.Price9.HasValue)
            Console.WriteLine($"  {record.Ticker9}: {record.Price9}");
        if (!string.IsNullOrEmpty(record.Ticker10) && record.Price10.HasValue)
            Console.WriteLine($"  {record.Ticker10}: {record.Price10}");
    }
    }

      //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Function #3 - Rull Full Pipeline
      // Inputs: This Function takes no arguments.
      // Actions: This function processes all .CSVs into all four sub-tables. (GasHub, GasPriceRecords, GasTickerPrice, FilesProcessed)
      // Returns: This Function Returns Nothing Except a Status Message on the Pipeline.
      //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    
public static void RunFullPipeline()
{
    try
    {
        // Step 1: Get all data files
        var files = FileIO.GetDataFiles();

        // Step 2: Convert CSV files to JSON
        string jsonOutput = FileIO.ProcessCsvFilesToJson();

        // Step 3: Save records to GasPriceRecord table
        FileIO.PostJsonToDatabase(jsonOutput);

        // Step 4: Insert new tickers into Gashub table
        FileIO.InsertNewTickersIntoGashub(jsonOutput);

        // Step 5: Populate GasTickerPrice table
        FileIO.PopulateGasTickerPrice(jsonOutput);

        // ✅ CLI output
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Pipeline executed successfully.");
        Console.ResetColor();

        Console.WriteLine($"Files processed: {files.FileCount}");
        foreach (var f in files.FileNames)
            Console.WriteLine($"  - {f}");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error(5): Pipeline failed + {ex.Message}");
        Console.ResetColor();
    }
}

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       // Function #4 - ShowPrices()
       // Inputs: This Function takes no arguments and does not restrict the output.
       // Actions: This function prints the PriceRecordDetails to the Screen for all records in the system without restriction.
       // Returns: This Function Returns Nothing But Writes the Output to System Console (Which when the CLI is running the API as well will include diagnostic calls for every API call).
   //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
//SUPPORTS CLI OPTIONS MENU TO SHOW PRICES FOR FIRST FIVE DAYS.
public static void ShowPrices()
{
    using (var context = new GashubContext())
    {
        var firstFiveDays = context.GasPriceRecords
            .OrderBy(r => r.RecordDate)
            .Take(5)
            .ToList();

        foreach (var rec in firstFiveDays)
        {
            // ✅ Set console text color to light blue
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Print Date and Daily Average in light blue
            Console.WriteLine($"Date: {rec.RecordDate:yyyy-MM-dd}");
            if (rec.DailyAverage.HasValue)
                Console.WriteLine($"  Avg Daily Price: {rec.DailyAverage:F2}");
            else
                Console.WriteLine("  Avg Daily Price: N/A");

            // Reset color back to default for tickers
            Console.ResetColor();

            // Then print up to 10 ticker/price pairs
            if (!string.IsNullOrEmpty(rec.Ticker1) && rec.Price1.HasValue)
                Console.WriteLine($"  {rec.Ticker1}: {rec.Price1}");
            if (!string.IsNullOrEmpty(rec.Ticker2) && rec.Price2.HasValue)
                Console.WriteLine($"  {rec.Ticker2}: {rec.Price2}");
            if (!string.IsNullOrEmpty(rec.Ticker3) && rec.Price3.HasValue)
                Console.WriteLine($"  {rec.Ticker3}: {rec.Price3}");
            if (!string.IsNullOrEmpty(rec.Ticker4) && rec.Price4.HasValue)
                Console.WriteLine($"  {rec.Ticker4}: {rec.Price4}");
            if (!string.IsNullOrEmpty(rec.Ticker5) && rec.Price5.HasValue)
                Console.WriteLine($"  {rec.Ticker5}: {rec.Price5}");
            if (!string.IsNullOrEmpty(rec.Ticker6) && rec.Price6.HasValue)
                Console.WriteLine($"  {rec.Ticker6}: {rec.Price6}");
            if (!string.IsNullOrEmpty(rec.Ticker7) && rec.Price7.HasValue)
                Console.WriteLine($"  {rec.Ticker7}: {rec.Price7}");
            if (!string.IsNullOrEmpty(rec.Ticker8) && rec.Price8.HasValue)
                Console.WriteLine($"  {rec.Ticker8}: {rec.Price8}");
            if (!string.IsNullOrEmpty(rec.Ticker9) && rec.Price9.HasValue)
                Console.WriteLine($"  {rec.Ticker9}: {rec.Price9}");
            if (!string.IsNullOrEmpty(rec.Ticker10) && rec.Price10.HasValue)
                Console.WriteLine($"  {rec.Ticker10}: {rec.Price10}");
        }
    }
}

     //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Function #5 - ShowHubs()
      // Inputs: None.
      // Actions: This Function shows the Hubs from all the Price Records and requires Unique Keys on the Ticker, and Name. Each Hub is essentially a Vendor for NewCo. It has sufficient attributes for addresses.
      // Returns: This Function Returns Nothing But Writes the Output to System Console (Which when the CLI is running the API as well will include diagnostic calls for every API call).
      //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    public static void ShowHubs()
    {
    using (var context = new GashubContext())
    {
        var hubs = context.Gashubs
            .Select(g => new { g.Id, g.GasTicker })
            .ToList();

        Console.WriteLine("Gas Hubs:");
        foreach (var hub in hubs)
        {
            Console.WriteLine($"- Id: {hub.Id}, Ticker: {hub.GasTicker}");
        }
    }
    }
    
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
      // Function #6 - ShowHubs()
      // Inputs: None.
      // Actions: This Function erases all the tables so that demonstrations can be redone, or the system retested for behavior after major modifications.
      // Returns: None.
      //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    
    public static void EraseDB()
    {
    try
    {
        using (var context = new GashubContext())
        {
            // Remove all rows from each table
            context.Apilogs.RemoveRange(context.Apilogs);
            context.FilesProcessed.RemoveRange(context.FilesProcessed);
            context.GasPriceRecords.RemoveRange(context.GasPriceRecords);
            context.GasTickerPrices.RemoveRange(context.GasTickerPrices);
            context.Gashubs.RemoveRange(context.Gashubs);

            context.SaveChanges();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("✅ All data erased from Apilog, FilesProcessed, GasPriceRecord, GasTickerPrice, and Gashub tables.");
        Console.ResetColor();
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error(EraseDB): Failed to erase database + {ex.Message}");
        Console.ResetColor();
    }
    }

}
