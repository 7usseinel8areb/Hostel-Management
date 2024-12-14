using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Models
{
    public class Feedback
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Message { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Rating from 1 to 5

        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
