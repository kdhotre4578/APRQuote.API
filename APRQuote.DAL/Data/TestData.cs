using APRQuote.DAL.Context;
using APRQuote.Core.Models;
using System.Collections.Generic;

namespace APRQuote.DAL.Data
{
    public class TestData
    {
        public void SeedDataAprQuote(AprQuoteDbContext context)
        {
            List<Vehicle> vehicles = new List<Vehicle>()
            {
                new Vehicle() { Make ="Audi", VehicleType = "Car" },
                new Vehicle() { Make ="Audi", VehicleType = "Van" },
                new Vehicle() { Make ="BMW", VehicleType = "Bike" }
            };

            vehicles.ForEach(v => context.Vehicles.Add(v));

            List<QuoteType> quoteTypes = new List<QuoteType>()
            {
                new QuoteType() { Type = "PCP" },
                new QuoteType() { Type = "HP" }
            };

            quoteTypes.ForEach(qt => context.QuoteTypes.Add(qt));

            List<APRPercentRange> aprPercentRanges = new List<APRPercentRange>()
            {
                new APRPercentRange() { ZeroThreeMonths = 5.0, ThreeSixMonths = 6.0, SixTwelveMonths = 7.0, TwelvePlusMonths = 8.0 },
                new APRPercentRange() { ZeroThreeMonths = 6.0, ThreeSixMonths = 7.0, SixTwelveMonths = 8.0, TwelvePlusMonths = 9.0 },
                new APRPercentRange() { ZeroThreeMonths = 4.0, ThreeSixMonths = 4.0, SixTwelveMonths = 4.0, TwelvePlusMonths = 4.0 }
            };

            aprPercentRanges.ForEach(a => context.APRPercentRanges.Add(a));

            context.SaveChanges();

            List<Quote> quotes = new List<Quote>()
            {
                new Quote() { VehicleId = 1, QuoteTypeId = 1, APRPercentRangeId = 1 },
                new Quote() { VehicleId = 1, QuoteTypeId = 2, APRPercentRangeId = 2 },
                new Quote() { VehicleId = 2, QuoteTypeId = 1, APRPercentRangeId = 3 },
                new Quote() { VehicleId = 3, QuoteTypeId = 2, APRPercentRangeId = 1 }
            };

            quotes.ForEach(q => context.Quotes.Add(q));

            context.SaveChanges();
        }
    }
}
