using System.ComponentModel.DataAnnotations;

namespace ResourceBookingCOJ.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100)]
        public string? Location {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Resource capacity MUST be a positive number")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Availability is required.")]
        public bool IsAvailable { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
