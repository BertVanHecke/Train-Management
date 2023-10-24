using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Station
    {
        public Station()
        {
            RideFromStations = new HashSet<Ride>();
            RideToStations = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string City { get; set; } = null!;

        public virtual ICollection<Ride> RideFromStations { get; set; }
        public virtual ICollection<Ride> RideToStations { get; set; }
    }
}
