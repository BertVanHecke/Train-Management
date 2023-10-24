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
    public class BookingService : IBookingService
    {
        private readonly IBookingDAO _bookingDAO;

        public BookingService(IBookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public async Task<bool> Add(Booking booking)
        {
            return await _bookingDAO.Add(booking);
        }

        public async Task<bool> Delete(Booking booking)
        {
            return await _bookingDAO.Delete(booking);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _bookingDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllByIdAsync(string id)
        {
            return await _bookingDAO.GetAllByIdAsync(id);
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _bookingDAO.GetByIdAsync(id);
        }

        public async Task<bool> Update(Booking booking)
        {
            return await _bookingDAO.Update(booking);
        }
    }
}
