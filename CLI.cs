/*namespace GasPriceAnalysis;

/// <summary>
/// Main program class for the Gas Price Analysis application
/// </summary>
class Program
{
  static async Task Main(string[] args)
  {
    Console.WriteLine("=== Gas Price Analysis - Extra Credit Assignment ===");
    Console.WriteLine();

    try
    {
      // TODO: Create instances of your services
      // Hint: You'll need to create a GasPriceContext, FileReaderService, and DatabaseService

      Console.WriteLine("Initializing database...");
      // TODO: Initialize the database

      Console.WriteLine("Reading data files...");
      // TODO: Get all CSV files from the Data directory
      // TODO: Process each file using FileReaderService
      // TODO: Save the data using DatabaseService

      Console.WriteLine("Data processing complete!");
      Console.WriteLine();

      // TODO: Implement a simple menu system for querying data
      await ShowMenuAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred: {ex.Message}");
      Console.WriteLine();
      Console.WriteLine("Stack trace:");
      Console.WriteLine(ex.StackTrace);
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
  }

  /// <summary>
  /// Shows a simple menu for querying gas price data
  /// </summary>
  static async Task ShowMenuAsync()
  {
    // TODO: Implement a simple menu system
    // Options could include:
    // 1. Show all gas hubs
    // 2. Show price history for a specific hub
    // 3. Show prices for a specific date
    // 4. Calculate average prices
    // 5. Exit
    var userInput = "";
    while (userInput != "4")
    {

      Console.WriteLine("=== Query Menu ===");
      Console.WriteLine("1. Show all gas hubs");
      Console.WriteLine("2. Show price history for a hub");
      Console.WriteLine("3. Show prices for a specific date");
      Console.WriteLine("4. Exit");
      userInput = Console.ReadLine();
      Console.WriteLine();
      Console.WriteLine("This menu is not implemented yet - students need to complete this!");
      Console.WriteLine("Hint: Use switch statements for menu logic");
    }

  }
}*/