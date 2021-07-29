# Luka Kovac - Volvo Challenge

This is the source code implementation for Volvo coding challenge

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
      ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
  4. Navigate to the API directory, launch the back end by running:
     ```
	 dotnet run
	 ```  
  6. Launch [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html) in your browser to view the API swagger documentation

## Technologies
* .NET Core 5
* ASP.NET Core

## Domain&Technical considerations related to this challenge
* It is inconclusive how clients will interact with the API (should request contain collection of DateTime where dates could be for different days/months...)
* for other cities, should the TollFreeVehicleProvider and TollFeeProvider be configurable? If yes, configuration file should be extended with additional properties.

## Provided solution technical risk list
Solution provided represents an idea to encapsulate business rules for calculating taxes into collection of linq predicates which will determine if given toll pass date is applicable for tax exemption.

 Solution is heavily using LINQ, in terms of performance it may cause some degraged experience for large set of data - [.net 5 linq performance.](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-5/)
 
 Also capturing context in Rule predicates is arguably a bad practice.

## Potential topics for discussion:
* Wrap business requirements into separate Rule classes - Composite Pattern.
* Configuration management - Support new features with 0 code changes.