using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class BookingTicket
    {
        public BookingTicket()
        {
            Seats = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public int BookingId { get; set; }
        public int TicketId { get; set; }
        public DateTime Date { get; set; }
        public int Class { get; set; }
        public bool Status { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Ticket Ticket { get; set; } = null!;
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
