using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APRQuote.DAL.Models
{
    [Table("TblQuoteType")]
    public class QuoteType
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        public string Type { get; set; }
    }
}
