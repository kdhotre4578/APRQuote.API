using NUnit.Framework;
using System.Collections.Generic;
using APRQuote.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using APRQuote.DAL.Models;
using APRQuote.Core.Models;
using System.Linq;
using APRQuote.DAL.Data;

namespace APRQuote.Test
{
    [TestFixture]
    public class AprQuoteControllerTests : ControllerTests
    {
        public AprQuoteControllerTests() : base()
        {
        }

        #region Get - Positive Scenarios

        [Test]
        public void GetQuoteAll_EmptyDB_Returns_EmptyList()
        {
            var controller = GetController();

            var response = controller.Get();
            var quotes = response.Result as ObjectResult;
            IEnumerable<AprQuote> quoteList = quotes.Value as IEnumerable<AprQuote>;

            Assert.AreEqual(0, quoteList.Count());
        }

        [TestCase(1)]
        public void GetQuote_EmptyDB_Input_Id_Returns_EmptyDataSet_Ok(int id)
        {
            var controller = GetController();

            var response = controller.Get(id);
            var quotes = response.Result as ObjectResult;
            AprQuote quote = quotes.Value as AprQuote;

            Assert.IsNull(quote);
            Assert.IsInstanceOf<NotFoundObjectResult>(response.Result);
        }

        [Test]
        public void GetQuotesAll_SeededData_Returns_All()
        {
            var controller = GetController();
            this.SeedData();

            var response = controller.Get();
            var responseResult = response.Result as ObjectResult;
            var quotes = responseResult.Value as IEnumerable<AprQuote>;

            Assert.AreEqual(4, quotes.Count());
        }

        #endregion

        #region Get - Negative Scenarios

        [TestCase(0)]
        [TestCase(-1)]
        public void GetQuote_EmptyDB_Input_InvalidId_Returns_BadRequestObjectResult(int id)
        {
            var controller = GetController();

            var response = controller.Get(id);

            Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
        }

        #endregion

        #region Post - Positive Scenarios

        [Test]
        public void PostQuote_EmptyData_Input_NewQuote_Returns_OK()
        {
            var controller = GetController();

            AprQuote aprQuote = GetNewQuote();
            var response = controller.Post(aprQuote);

            Assert.IsInstanceOf<AcceptedResult>(response.Result);
        }

        [Test]
        public void PostQuote_EmptyData_Input_NewQuote_Twice_Returns_Conflict()
        {
            var controller = GetController();

            AprQuote aprQuote = GetNewQuote();
            var responsePost1 = controller.Post(aprQuote);
            var responsePost2 = controller.Post(aprQuote);

            Assert.IsInstanceOf<ConflictResult>(responsePost2.Result);
        }

        #endregion

        #region Post - Negative Scenarios

        [Test]
        public void PostQuote_EmptyData_Input_Null_Returns_BadRequest()
        {
            var controller = GetController();

            AprQuote aprQuote = null;
            var response = controller.Post(aprQuote);

            Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
        }

        [Test]
        public void PostQuote_Input_Multiple_InvalidFewFields_Returns_BadRequest()
        {
            var controller = GetController();

            // Invalid vehicle make
            AprQuote aprQuote;
            aprQuote = new AprQuote() { Make = "" };
            var response1 = controller.Post(aprQuote);

            Assert.IsInstanceOf<BadRequestObjectResult>(response1.Result);

            // Invalid QuoteType
            aprQuote = new AprQuote() { Make = "Lexus", 
                                        VehicleType = "Car", 
                                        QuoteType = "", 
                                        ZeroThreeMonths = 1.0, 
                                        ThreeSixMonths = 2.0, 
                                        SixTwelveMonths = 3.0, 
                                        TwelvePlusMonths = 4.0 };

            var response2 = controller.Post(aprQuote);

            Assert.IsInstanceOf<BadRequestObjectResult>(response2.Result);

            // Invalid APR => Zero Three Months

            aprQuote = new AprQuote()
            {
                Make = "Lexus",
                VehicleType = "Car",
                QuoteType = "PCP",
                ZeroThreeMonths = -1.0,
                ThreeSixMonths = 2.0,
                SixTwelveMonths = 3.0,
                TwelvePlusMonths = 4.0
            };

            var response3 = controller.Post(aprQuote);

            Assert.IsInstanceOf<BadRequestObjectResult>(response3.Result);
        }

        #endregion

        #region Complete - Get and Post together

        [Test]
        public void Complete_EmptyDB_Post_New_Quote_Get_Input_ValidQuote_Returns_NewAcceptedQuote()
        {
            var controller = GetController();

            AprQuote aprQuote = GetNewQuote();

            var postResponse = controller.Post(aprQuote);

            Assert.IsInstanceOf<AcceptedResult>(postResponse.Result);

            var getResponse = controller.Get();
            var responseResult = getResponse.Result as ObjectResult;
            var quotes = responseResult.Value as IEnumerable<AprQuote>;

            Assert.AreEqual(1, quotes.Count());
        }

        [Test]
        public void Complete_SeededDB_Post_New_Quote_Get_Input_ValidQuote_Returns_All()
        {
            var controller = GetController();

            this.SeedData();

            AprQuote aprQuote = GetNewQuote();

            var postResponse = controller.Post(aprQuote);

            Assert.IsInstanceOf<AcceptedResult>(postResponse.Result);

            var getResponse = controller.Get();
            var responseResult = getResponse.Result as ObjectResult;
            var quotes = responseResult.Value as IEnumerable<AprQuote>;

            Assert.AreEqual(5, quotes.Count());
        }

        #endregion

        #region Helper Methods

        private AprQuoteController GetController()
        {
            return new AprQuoteController(new AprContextUoW(context));
        }

        private void SeedData()
        {
            new TestData().SeedDataAprQuote(context);
        }

        private AprQuote GetNewQuote()
        {
            return new AprQuote()
            {
                Make = "Lexus",
                VehicleType = "Car",
                QuoteType = "PCP",
                ZeroThreeMonths = 4.1,
                ThreeSixMonths = 5.1,
                SixTwelveMonths = 7.1,
                TwelvePlusMonths = 8.1
            };
        }

        #endregion
    }
}
