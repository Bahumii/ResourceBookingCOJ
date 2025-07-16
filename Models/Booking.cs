using System.ComponentModel.DataAnnotations;

namespace ResourceBookingCOJ.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Resource is required.")]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Start date and time is required.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End date and time is required.")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "User booking is required.")]
        public string? BookedBy { get; set; }

        [Required(ErrorMessage = "Purpose is required.")]
        public string? Purpose { get; set; }

        public Resource? Resource { get; set; }
    }
}
