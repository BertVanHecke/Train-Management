using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Ride
    {
        public Ride()
        {
            Seats = new HashSet<Seat>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int TrainId { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Distance { get; set; }

        public virtual Station FromStation { get; set; } = null!;
        public virtual Station ToStation { get; set; } = null!;
        public virtual Train Train { get; set; } = null!;
        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
