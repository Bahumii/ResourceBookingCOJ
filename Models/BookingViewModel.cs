namespace ResourceBookingCOJ.Models
{
    // booking view model that will hold model for viewing for bookings and resource and new booking model for booking creation
    // model also holds search name and date for booking index page
    public class BookingViewModel
    {
        public Booking NewBooking { get; set; } = new Booking();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<Resource> Resources { get; set; } = new List<Resource>();

        public string? SearchName { get; set; }
        public DateTime? SearchDate { get; set; }
    }
}
