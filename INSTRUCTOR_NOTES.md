# Instructor Notes - Gas Price Analysis Assignment

## Overview
This extra credit assignment is designed to test students' understanding of C# fundamentals and Entity Framework Core. The project structure is set up as a scaffold that students need to complete.

## Assessment Rubric

### Code Quality (25 points)
- Clean, readable code with proper naming conventions
- Appropriate use of comments and documentation
- Proper error handling and validation
- Following C# coding standards

### Functionality (40 points)
- Database context properly configured (10 points)
- File reading service working correctly (10 points)
- Database service implementing all CRUD operations (15 points)
- Main program integrating all components (5 points)

### C# Fundamentals (25 points)
- Proper use of classes, properties, and methods (8 points)
- Correct implementation of collections and LINQ (8 points)
- Appropriate exception handling (5 points)
- File I/O operations implemented correctly `using` keyword (4 points)

### Entity Framework Usage (10 points)
- Correct entity relationships and constraints (5 points)
- Proper use of DbContext and DbSet (3 points)
- Async operations implemented correctly (2 points)

## Common Student Mistakes to Watch For

1. **Connection String Issues**: Students often forget to configure the SQLite connection properly
2. **File Path Problems**: Hardcoding paths instead of using relative paths or Path.Combine
3. **Missing Error Handling**: Not handling file not found, parsing errors, or database exceptions
4. **Entity Relationship Issues**: Forgetting to configure foreign keys or navigation properties
5. **Async/Await Misuse**: Not properly awaiting async operations or blocking on async calls

## Extension Opportunities

Bonus challenges:
- Implement data validation and business rules
- Add logging using ILogger
- Implement configuration using appsettings.json

## Prerequisites
Students should be comfortable with:
- C# classes, properties, and methods
- Collections (List<T>, Dictionary<TKey, TValue>)
- Basic LINQ operations
- File I/O concepts
- Basic understanding of databases and SQL