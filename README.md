# Gas Price Data Analysis - Extra Credit Assignment

## Overview
This extra credit assignment will test your understanding of C# fundamentals, file I/O operations, and Entity Framework Core. You'll build a console application that reads gas price data from semicolon-delimited files and stores the information in a database.

## Assignment Description
Your task is to create a C# console application that:
1. Reads gas price data from multiple CSV-like files (semicolon-delimited)
2. Parses the data and creates appropriate data models
3. Uses Entity Framework Core to store the data in a database
4. Provides basic querying capabilities to analyze the gas price trends

## Data Format
Each input file represents gas prices for a specific day and contains the following format:
```
GasHubTicker;Price
HH;2.45
NGPL_TexOk;2.38
Katy;2.42
```

- **GasHubTicker**: A string representing the gas trading hub identifier
- **Price**: A decimal value representing the price per unit for that day

## Project Structure

`EXAMPLE`
```
├── Program.cs              # Main entry point
├── Models/
│   ├── GasHub.cs          # Gas hub entity model
│   ├── GasPriceRecord.cs  # Gas price record entity model
│   └── GasPriceContext.cs # Entity Framework DbContext
├── Services/
│   ├── FileReaderService.cs    # File reading logic
│   └── DatabaseService.cs     # Database operations
├── Data/                  # Sample data files
│   ├── gas_prices_2024_11_01.csv
│   ├── gas_prices_2024_11_02.csv
│   ├── gas_prices_2024_11_03.csv
│   ├── gas_prices_2024_11_04.csv
│   └── gas_prices_2024_11_05.csv
└── README.md
```

## Requirements

### Part 1: Data Models 
Create appropriate entity models for:
- **GasHub**: Represents a gas trading hub with properties for ID, Ticker, and Name
- **GasPriceRecord**: Represents a price record with properties for ID, GasHubId (foreign key), Price, and Date

### Part 2: Database Context 
- Configure Entity Framework DbContext
- Set up proper relationships between entities
- Configure the database provider

### Part 3: File Reading Service 
- Implement a service to read semicolon-delimited files
- Parse data and handle potential errors gracefully
- Extract date information from filename

### Part 4: Database Operations 
- Implement methods to save gas price data
- Create functionality to query price trends (EX: Querying prices below a certain amount on this day, getting the gas hub tickers as results)
- Ensure data integrity and handle duplicates appropriately

## Getting Started

1. **Install Required Packages**: Ensure you have Entity Framework Core packages
2. **Create Database Models**: Start by implementing the entity classes in the Models folder
3. **Set Up DbContext**: Configure your database context with proper entity relationships
4. **Implement File Reading**: Create a service to read and parse the semicolon-delimited files
5. **Build the Main Application**: Tie everything together in Program.cs

## Sample Data Files
The project includes 5 sample data files with gas price information for different trading hubs across 5 consecutive days.

Feel free to include your own as well!

## Bonus Challenges (Extra Points)
- **Data Validation**: Implement robust data validation for price values and ticker formats (How should the ticker symbols look? What happens if price data is negative? what if there are special characters?)
- **Error Logging**: Implement proper logging for file reading and database operations (looking for a logging service)
- **Configuration**: Use configuration files for database connection strings and file paths (not hard coded)

**Total: 20 extra points**

## Submission Guidelines
1. Ensure your project compiles and runs without errors
2. Include proper error handling throughout your application
3. Comment your code appropriately
4. Test with the provided sample data files
5. Submit your complete project folder

## Evaluation Criteria
- **Code Quality**: Clean, readable, and well-organized code
- **Functionality**: Application works as specified
- **Error Handling**: Proper exception handling and validation
- **Entity Framework Usage**: Correct implementation of EF Core concepts
- **C# Fundamentals**: Demonstration of core programming concepts


---
**Due Date**: 12/01/2025  
**Points**: 100 points + bonus opportunities  
**Estimated Time**: 4-8 hours