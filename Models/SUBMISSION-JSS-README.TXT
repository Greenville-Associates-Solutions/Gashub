This is a Reverse Engineered Database in SQLLITE.

01)DBContext, and Models Stored in Models.
02)Program.cs runs both the CLI, and the REST-EPs. (A First for Me)
03)File.IO includes the import functions which read .CSV's and process JSONS to the Database.
04)Data contains the Instructors .CSV Files for import
05)SQLDATA contains the database under test.
06)SQLITE contains the Windows SQLITE executable.
07)The original instructors files, and my original Rest Program.CS which were by themselves were moved to SERVICES-> WIP-OLD-SCRAP just in commented form.
08)Services contains the CLI Command Functions, and FileIO commands.
09)Controllers contains the Rest Endpoints for each Table. There is 1 file and 4 REST endpoints for each Table in the Database.
10)Models contains the database schema for the SQLLite Databases. There are 4 Tables in the Design (Apilog, Gashub,GasPriceRecord,GasTickerPrice)
11)Average Prices are being recoreded on input of the Daily records which are mapped like for like to a DB record in GasPriceRecord.
12)Each Ticker Symbol is then being loaded into Gashub by a seperate process which checks for a Unique Ticker-Company Name combination.
13) We built a single HTML file to show the results from two calls 1) To GasHub to get all of the Tickers in the System 2) To GasTickerPrice which shows the history for Prices.
14) Apis are at 1)/api/Gashub 2) /api/GasTickerPrice 3) /api/GasPriceRecord (Single Day Records) 4) /api/Apilog 5) /api/GasActions which does all the CLI commands in GridAction form.
15) SwaggerPDF Documentation in SwaggerDocs.