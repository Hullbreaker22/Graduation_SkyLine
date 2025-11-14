using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyLine.Models
{


    public enum Status
    {
        Pending,
        Completed,
        Cancelled
    }


    public class Booking
    {

        [Key]
        public int Booking_Id_PK { get; set; }

        [ForeignKey(nameof(User))]
        public string User_Id_FK { get; set; } = string.Empty;

        public Status status { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? PNR { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
