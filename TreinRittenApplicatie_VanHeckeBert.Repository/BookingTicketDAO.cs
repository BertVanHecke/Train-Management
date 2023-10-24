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
    public class BookingTicketDAO : IBookingTicketDAO
    {
        private readonly TrainRideDbContext _context;

        public BookingTicketDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(BookingTicket bookingTicket)
        {
            _context.Add(bookingTicket);
            return await Save();
        }

        public async Task<bool> Delete(BookingTicket bookingTicket)
        {
            _context.Remove(bookingTicket);
            return await Save();
        }

        public async Task<IEnumerable<BookingTicket>> GetAllAsync()
        {
            try
            {
                return await _context.BookingTickets.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<IEnumerable<BookingTicket>> GetBookingTicketStatus(int id)
        {
            try
            {
                return await _context.BookingTickets.Where(bt => bt.Status == true).Where(bt => bt.BookingId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<BookingTicket> GetByIdAsync(int id)
        {
            try
            {
                return await _context.BookingTickets.FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO");
            }
        }

        public async Task<IEnumerable<BookingTicket>> GetTicketsForARideFilteredByDate(int id, DateTime date)
        {
            try
            {
                return await _context.BookingTickets.Include(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.Seats).Where(bt => bt.Date == date).Where(bt => bt.TicketId == id).ToListAsync();
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

        public async Task<bool> Update(BookingTicket bookingTicket)
        {
            _context.Update(bookingTicket);
            return await Save();
        }
    }
}
