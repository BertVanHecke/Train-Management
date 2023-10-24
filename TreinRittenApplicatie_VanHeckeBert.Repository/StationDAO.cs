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
    public class StationDAO : IDAO<Station>
    {
        private readonly TrainRideDbContext _context;

        public StationDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Station station)
        {
            _context.Add(station);
            return await Save();
        }

        public async Task<bool> Delete(Station station)
        {
            _context.Remove(station);
            return await Save();
        }

        public async Task<IEnumerable<Station>> GetAllAsync()
        {
            try
            {
                return await _context.Stations.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in StationDAO: " + ex.Message);
                throw new Exception("Error StationDAO");
            }
        }

        public async Task<Station> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Stations.FirstOrDefaultAsync(st => st.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in StationDAO: " + ex.Message);
                throw new Exception("Error StationDAO");
            }
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Station station)
        {
            _context.Update(station);
            return await Save();
        }
    }
}
