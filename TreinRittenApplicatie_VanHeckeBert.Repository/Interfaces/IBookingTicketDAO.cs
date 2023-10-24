using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.Repository.Interfaces
{
    public interface IBookingTicketDAO : IDAO<BookingTicket>
    {
        Task<IEnumerable<BookingTicket>> GetTicketsForARideFilteredByDate(int id, DateTime date);
        Task<IEnumerable<BookingTicket>> GetBookingTicketStatus(int id);
    }
}
