using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyLine.Models
{

    public class Fare
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(flight))]
        public  int flightId { get; set; }
        public Flight? flight { get; set; }
        public Cabin CabinClass { get; set; }
        public double price { get; set; }
        public bool Refundable { get; set; }
        public bool Changeable { get; set; }
        public string Baggage_Allowance { get; set; }
        public bool Seat_Selection { get; set; }


    }
}
