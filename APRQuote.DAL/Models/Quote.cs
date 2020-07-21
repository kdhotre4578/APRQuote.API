using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APRQuote.DAL.Models
{
    [Table("TblQuote")]
    public class Quote
    {
        [Key]
        public int Id { get; set; }

        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle vehicle { get; set; }

        public int QuoteTypeId { get; set; }

        [ForeignKey("QuoteTypeId")]
        public QuoteType quoteType { get; set; }

        public int APRPercentRangeId { get; set; }
        
        [ForeignKey("APRPercentRangeId")]
        public APRPercentRange aprPercentRange { get; set; }
    }
}
