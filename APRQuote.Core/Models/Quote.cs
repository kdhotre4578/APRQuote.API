using System;

namespace APRQuote.Core.Models
{
    public class Quote
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public Vehicle vehicle { get; set; }

        public int QuoteTypeId { get; set; }

        public QuoteType quoteType { get; set; }

        public int APRPercentRangeId { get; set; }

        public APRPercentRange aprPercentRange { get; set; }
    }
}
