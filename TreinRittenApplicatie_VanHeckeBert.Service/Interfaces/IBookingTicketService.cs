using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.Service.Interfaces
{
    public interface IBookingTicketService : IService<BookingTicket>
    {
        Task<IEnumerable<BookingTicket>> GetTicketsForARideFilteredByDate(int id, DateTime date);
        Task<IEnumerable<BookingTicket>> GetBookingTicketStatus(int id);

    }
}
