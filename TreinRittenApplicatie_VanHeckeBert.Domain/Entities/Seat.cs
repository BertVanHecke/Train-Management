using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Seat
    {
        public int Id { get; set; }
        public int? BookingTicketId { get; set; }
        public int? RideId { get; set; }
        public int? Number { get; set; }

        public virtual BookingTicket? BookingTicket { get; set; }
        public virtual Ride? Ride { get; set; }
    }
}
