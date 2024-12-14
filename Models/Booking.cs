using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Models
{
    public class Booking
    {
        public ObjectId Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public double TotalAmount { get; set; } = 0;
    }
}
