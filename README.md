# APRQuote.API
AprQuote.API is REST API which retrieves and adds APR Quote(s).
----------------------------------------------------------------------------------------------
Application Structure :
 
AprQuote.API
 - Contains Get and Post api request of APR quote(s).
 - Utilised .Net Core Dependency Injection (IoC Container) for loose coupling between API, BL and DAL.
 - Contains exception handler middleware

AprQuote.Core
- Contains Respository and Unit of Work interfaces.
- Contains AprQuote interface.
- Contains models.

AprQuote.BLayer
- Contains the service class which has logic to retrieve and insert the quotes.
- Injected the DAL dependencies, retrieved the quotes and implemented UoW pattern to post data to Database. 
- Validation logic for the quote.

AprQuote.DAL
- Implemented using Enity Framework Core Code First Approach.
- Contains generic Repository and UnitOfWork (pattern) class implementation.
- Contains configuration class to create table entities based on AprQuote.Core models.

AprQuote.Tests
 - Utilised NUnit framework for unit testing.
 - Created a in-memory database to perform testing with sample seeded data.
 - Contains both positive and negative scenarios.

----------------------------------------------------------------------------------------------
Technologies :

 - Programming Language : C#
 - Back End : ASP.NET Core Web API (.Net Core 3.1)
 - Database : Sql Server
 - Unit Test Framework : NUnit
 - Data Access : Entity Framework Core (Code First Approach)
