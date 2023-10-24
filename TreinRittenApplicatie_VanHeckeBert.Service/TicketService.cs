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
    public class TicketService : IService<Ticket>
    {

        private readonly IDAO<Ticket> _ticketDAO;

        public TicketService(IDAO<Ticket> ticketDAO)
        {
            _ticketDAO = ticketDAO;
        }

        public async Task<bool> Add(Ticket ticket)
        {
            return await _ticketDAO.Add(ticket);
        }

        public async Task<bool> Delete(Ticket ticket)
        {
            return await _ticketDAO.Delete(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketDAO.GetAllAsync();
        }

        public Task<IEnumerable<Ticket>> GetAllByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await _ticketDAO.GetByIdAsync(id);
        }

        public async Task<bool> Update(Ticket ticket)
        {
            return await _ticketDAO.Update(ticket);
        }
    }
}
