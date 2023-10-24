using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Data;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces;

namespace TreinRittenApplicatie_VanHeckeBert.Repository
{
    public class RideDAO : IRideDAO
    {
        private readonly TrainRideDbContext _context;

        public RideDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Ride ride)
        {
            _context.Add(ride);
            return await Save();
        }

        public async Task<bool> Delete(Ride ride)
        {
            _context.Remove(ride);
            return await Save();
        }

        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            try
            {
                return await _context.Rides.Include(r => r.FromStation).Include(r => r.ToStation).Include(r => r.Train).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<Ride> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Rides.Include(r => r.FromStation).Include(r => r.ToStation).Include(r => r.Train).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<Ride> GetTrainForStartAndStopStation(int fromStationId, int toStationId)
        {
            return await _context.Rides.Include(r => r.Train)
                .Where(r => r.FromStationId == fromStationId)
                .Where(r => r.ToStationId == toStationId).FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Ride ride)
        {
            _context.Update(ride);
            return await Save();
        }
    }
}
