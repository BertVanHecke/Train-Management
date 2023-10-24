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
    public class BookingTicketService : IBookingTicketService
    {
        private readonly IBookingTicketDAO _bookingTicketDAO;

        public BookingTicketService(IBookingTicketDAO bookingTicketDAO)
        {
            _bookingTicketDAO = bookingTicketDAO;
        }

        public async Task<bool> Add(BookingTicket bookingTicket)
        {
            return await _bookingTicketDAO.Add(bookingTicket);
        }

        public async Task<bool> Delete(BookingTicket bookingTicket)
        {
            return await _bookingTicketDAO.Delete(bookingTicket);
        }

        public async Task<IEnumerable<BookingTicket>> GetAllAsync()
        {
            return await _bookingTicketDAO.GetAllAsync();
        }

        public async Task<IEnumerable<BookingTicket>> GetBookingTicketStatus(int id)
        {
            return await _bookingTicketDAO.GetBookingTicketStatus(id);
        }

        public async Task<BookingTicket> GetByIdAsync(int id)
        {
            return await _bookingTicketDAO.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BookingTicket>> GetTicketsForARideFilteredByDate(int id, DateTime date)
        {
            return await _bookingTicketDAO.GetTicketsForARideFilteredByDate(id, date);
        }

        public async Task<bool> Update(BookingTicket bookingTicket)
        {
            return await _bookingTicketDAO.Update(bookingTicket);
        }
    }
}
