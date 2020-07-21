using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APRQuote.DAL.Models
{
    [Table("TblAPRPercentRanges")]
    public class APRPercentRange
    {
        [Key]
        public int Id { get; set; }

        public double ZeroThreeMonths { get; set; }

        public double ThreeSixMonths { get; set; }

        public double SixTwelveMonths { get; set; }

        public double TwelvePlusMonths { get; set; }
    }
}
