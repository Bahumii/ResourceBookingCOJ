using System.ComponentModel.DataAnnotations;

namespace ResourceBookingCOJ.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Location {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Resource capacity MUST be a positive number")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<Booking>? Bookings { get; set; }
    }
}
