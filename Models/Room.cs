using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Models
{
    public class Room
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomNumber { get; set; }

        [Required]
        public string RoomType { get; set; } // E.g., "Single", "Double"

        [Required]
        [Range(1, 100)]
        public int Capacity { get; set; }

        [Range(0, double.MaxValue)]
        public double PricePerNight { get; set; }
    }
}
