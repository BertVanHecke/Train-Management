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
    public class SeatService : IService<Seat>
    {
        private readonly IDAO<Seat> _seatDAO;

        public SeatService(IDAO<Seat> seatDAO)
        {
            _seatDAO = seatDAO;
        }

        public async Task<bool> Add(Seat seat)
        {
            return await _seatDAO.Add(seat);
        }

        public async Task<bool> Delete(Seat seat)
        {
            return await _seatDAO.Delete(seat);
        }

        public async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await _seatDAO.GetAllAsync();
        }

        public Task<IEnumerable<Seat>> GetAllByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            return await _seatDAO.GetByIdAsync(id);
        }

        public async Task<bool> Update(Seat seat)
        {
            return await _seatDAO.Update(seat);
        }
    }
}
