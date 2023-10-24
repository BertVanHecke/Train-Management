using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces
{
    public interface IRideDAO : IDAO<Ride>
    {
        Task<Ride> GetTrainForStartAndStopStation(int fromStationId, int toStationId);
    }
}
