using System;
using System.Collections.Generic;

namespace TreinRittenApplicatie_VanHeckeBert.Domain.Entities
{
    public partial class Train
    {
        public Train()
        {
            Rides = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
