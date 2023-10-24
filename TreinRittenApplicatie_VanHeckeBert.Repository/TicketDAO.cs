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
    public class TicketDAO : IDAO<Ticket>
    {
        private readonly TrainRideDbContext _context;

        public TicketDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Ticket ticket)
        {
            _context.Add(ticket);
            return await Save();
        }

        public async Task<bool> Delete(Ticket ticket)
        {
            _context.Remove(ticket);
            return await Save();
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            try
            {
                return await _context.Tickets.Include(t => t.Rides).ThenInclude(r => r.FromStation)
                    .Include(t => t.Rides).ThenInclude(r => r.ToStation)
                    .Include(t => t.Rides).ThenInclude(r => r.Train)
                    .Include(t => t.Rides.OrderBy(r => r.DepartureTime))   
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TicketDAO: " + ex.Message);
                throw new Exception("Error TicketDAO");
            }
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Tickets.Include(t => t.Rides).ThenInclude(r => r.FromStation)
                    .Include(t => t.Rides).ThenInclude(r => r.ToStation)
                    .Include(t => t.Rides).ThenInclude(r => r.Train).Include(t => t.Rides.OrderBy(r => r.DepartureTime)).FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in TicketDAO: " + ex.Message);
                throw new Exception("Error TicketDAO");
            }
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> Update(Ticket ticket)
        {
            _context.Update(ticket);
            return await Save();
        }
    }
}
