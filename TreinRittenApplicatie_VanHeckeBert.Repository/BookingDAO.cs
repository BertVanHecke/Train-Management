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
    public class BookingDAO : IBookingDAO
    {
        private readonly TrainRideDbContext _context;

        public BookingDAO(TrainRideDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Booking booking)
        {
            _context.Add(booking);
            return await Save();
        }

        public async Task<bool> Delete(Booking booking)
        {
            _context.Remove(booking);
            return await Save();
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            try
            {
                return await _context.Bookings
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.FromStation)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.ToStation)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.Train)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Seats).ThenInclude(s => s.Ride).ThenInclude(r => r.Seats).ThenInclude(s => s.BookingTicket)
                    .Include(b => b.AspNetUser)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO " + ex.Message);
            }
        }

        public async Task<IEnumerable<Booking>> GetAllByIdAsync(string id)
        {
            try
            {
                return await _context.Bookings
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.FromStation)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.ToStation)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides).ThenInclude(r => r.Train)
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Ticket).ThenInclude(t => t.Rides.OrderBy(r => r.DepartureTime))
                    .Include(b => b.BookingTickets).ThenInclude(bt => bt.Seats).ThenInclude(s => s.Ride).ThenInclude(r => r.Seats).ThenInclude(s => s.BookingTicket)
                    .Include(b => b.AspNetUser).Where(b => b.AspNetUserId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in RideDAO: " + ex.Message);
                throw new Exception("Error RideDAO " + ex.Message);
            }
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Bookings.FirstOrDefaultAsync(r => r.Id == id);
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

        public async Task<bool> Update(Booking booking)
        {
            _context.Update(booking);
            return await Save();
        }
    }
}
