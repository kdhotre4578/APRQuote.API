using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APRQuote.DAL.Models
{
    [Table("TblVehicle")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        public string Make { get; set; }

        [StringLength(25)]
        public string VehicleType { get; set; }
    }
}
