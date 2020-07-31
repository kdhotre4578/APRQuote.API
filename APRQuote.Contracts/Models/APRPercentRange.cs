using System;

namespace APRQuote.Core.Models
{
    public class APRPercentRange
    {
        public int Id { get; set; }

        public double ZeroThreeMonths { get; set; }

        public double ThreeSixMonths { get; set; }

        public double SixTwelveMonths { get; set; }

        public double TwelvePlusMonths { get; set; }
    }
}
