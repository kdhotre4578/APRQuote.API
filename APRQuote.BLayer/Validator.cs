using APRQuote.Core.Models;

namespace APRQuote.BLayer
{
    public class Validator
    {
        public bool IsValid(AprQuote quote)
        {
            if (quote == null
                || string.IsNullOrEmpty(quote.Make)
                || string.IsNullOrEmpty(quote.VehicleType)
                || string.IsNullOrEmpty(quote.QuoteType)
                || quote.ZeroThreeMonths < 0
                || quote.ThreeSixMonths < 0
                || quote.SixTwelveMonths < 0
                || quote.TwelvePlusMonths < 0)
            {
                return false;
            }

            return true;
        }
    }
}
