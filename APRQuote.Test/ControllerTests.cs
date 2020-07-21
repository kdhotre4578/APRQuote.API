using NUnit.Framework;
using System;
using APRQuote.DAL.Context;
using Microsoft.EntityFrameworkCore;


namespace APRQuote.Test
{
    [TestFixture]
    public class ControllerTests : IDisposable
    {
        protected AprQuoteDbContext context;

        public ControllerTests()
        {
            this.InitializeContext();
        }
        
        [SetUp]
        protected void InitializeContext()
        {
            var options = new DbContextOptionsBuilder<AprQuoteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            context = new AprQuoteDbContext(options);

            context.Database.EnsureCreated();
        }

        [TearDown]
        protected void Release()
        {
            context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
