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
    public class TrainDAO : IDAO<Train>
    {
        private readonly TrainRideDbContext _context;

        public TrainDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Train train)
        {
            _context.Add(train);
            return await Save();
        }

        public async Task<bool> Delete(Train train)
        {
            _context.Remove(train);
            return await Save();
        }

        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            try
            {
                return await _context.Trains.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TrainDAO: " + ex.Message);
                throw new Exception("Error TrainDAO");
            }
        }

        public async Task<Train> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Trains.FirstOrDefaultAsync(tr => tr.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TrainDAO: " + ex.Message);
                throw new Exception("Error TrainDAO");
            }
        }


        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Train train)
        {
            _context.Update(train);
            return await Save();
        }
    }
}
