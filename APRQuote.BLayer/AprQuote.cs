using System;

namespace APRQuote.BLayer
{
    public class AprQuote
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string VehicleType { get; set; }

        public string QuoteType { get; set; }

        public double ZeroThreeMonths { get; set; }

        public double ThreeSixMonths { get; set; }

        public double SixTwelveMonths { get; set; }

        public double TwelvePlusMonths { get; set; }
    }
}
