using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;

namespace TreinRittenApplicatie_VanHeckeBert.Service
{
    public class RideService : IRideService
    {

        private readonly IRideDAO _rideDAO;

        public RideService(IRideDAO rideDAO)
        {
            _rideDAO = rideDAO;
        }

        public async Task<bool> Add(Ride ride)
        {
            return await _rideDAO.Add(ride);
        }

        public async Task<bool> Delete(Ride ride)
        {
            return await _rideDAO.Delete(ride);
        }

        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            return await _rideDAO.GetAllAsync();
        }

        public Task<IEnumerable<Ride>> GetAllByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Ride> GetByIdAsync(int id)
        {
            return await _rideDAO.GetByIdAsync(id);
        }

        public async Task<Ride> GetTrainForStartAndStopStation(int fromStationId, int toStationId)
        {
            return await _rideDAO.GetTrainForStartAndStopStation(fromStationId, toStationId);
        }

        public async Task<bool> Update(Ride ride)
        {
            return await _rideDAO.Update(ride);
        }
    }
}
