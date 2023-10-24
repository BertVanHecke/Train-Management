namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class BookingTicketViewModel
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int TicketId { get; set; }
        public DateTime Date { get; set; }
        public int Class { get; set; }
        public bool Status { get; set; }
        public TicketViewModel Ticket { get; set; }
        public BookingViewModel Booking { get; set; }
        public IEnumerable<SeatViewModel> Seats { get; set; }
    }
}
