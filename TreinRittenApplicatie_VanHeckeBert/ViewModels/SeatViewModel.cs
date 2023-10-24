namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class SeatViewModel
    {
        public int Id { get; set; }
        public int? BookingTicketId { get; set; }
        public int? RideId { get; set; }
        public int? Number { get; set; }
        public BookingTicketViewModel BookingTicket { get; set; }
        public RideViewModel Ride { get; set; }
    }
}
