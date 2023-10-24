namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string AspNetUserId { get; set; } = null!;
        public bool Status { get; set; }
        public string AspNetUserFirstName { get; set; }
        public string AspNetUserLastName { get; set; }
        public IEnumerable<BookingTicketViewModel> BookingTickets { get; set; }
    }
}
