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
    public class SeatDAO : IDAO<Seat>
    {
        private readonly TrainRideDbContext _context;

        public SeatDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Seat seat)
        {
            _context.Add(seat);
            return await Save();
        }

        public async Task<bool> Delete(Seat seat)
        {
            _context.Remove(seat);
            return await Save();
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            try
            {
                return await _context.Seats.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Seats.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }


        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Seat seat)
        {
            _context.Update(seat);
            return await Save();
        }
    }
}

