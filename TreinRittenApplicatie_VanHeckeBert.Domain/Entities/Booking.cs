using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingTickets = new HashSet<BookingTicket>();
        }

        public int Id { get; set; }
        public string AspNetUserId { get; set; } = null!;
        public bool Status { get; set; }

        public virtual AspNetUser AspNetUser { get; set; } = null!;
        public virtual ICollection<BookingTicket> BookingTickets { get; set; }
    }
}
