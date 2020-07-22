# APRQuote.API
AprQuote.API is REST API which retrieves and adds APR Quote(s).
----------------------------------------------------------------------------------------------
Application Structure :
 
AprQuote.API
 - Contains Post and Add request of APR quote(s).
 - Utilised .Net Core Dependency Injection (IoC Container) for loose coupling between API and DAL.
 - Contains exception handler middleware

AprQuote.Contracts
- Contains Respository and Unit of Work interfaces.

AprQuote.BLayer
- Contains the classes which has logic to retrieve and insert the quotes.
- Injected the DAL dependencies, retrieved the quotes and implemented UoW pattern to post data to Database. 
- View model / DTO class for quote.
- Validation logic for the quote.

AprQuote.DAL
- Implemented using Enity Framework Core Code First Approach.
- Contains generic Repository and UnitOfWork class implementation.
- Contains table entities.

AprQuote.Tests
 - Utilised NUnit framework for unit testing.
 - Created a in-memory database to perform testing with sample data.
 - Contains both positive and negative scenarios.

----------------------------------------------------------------------------------------------
Technologies :

 - Programming Language : C#
 - Back End : ASP.NET Core Web API (.Net Core 3.1)
 - Database : Sql Server
 - Unit Test Framework : NUnit
 - Data Access : Entity Framework Core (Code First Approach)
