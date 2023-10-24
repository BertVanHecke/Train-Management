using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            BookingTickets = new HashSet<BookingTicket>();
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public decimal PriceEconomic { get; set; }
        public decimal PriceBusiness { get; set; }

        public virtual ICollection<BookingTicket> BookingTickets { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
