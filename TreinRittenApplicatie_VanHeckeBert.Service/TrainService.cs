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
    public class TrainService : IService<Train>
    {

        private readonly IDAO<Train> _trainDAO;

        public TrainService(IDAO<Train> trainDAO)
        {
            _trainDAO = trainDAO;
        }

        public async Task<bool> Add(Train train)
        {
            return await _trainDAO.Add(train);
        }

        public async Task<bool> Delete(Train train)
        {
            return await _trainDAO.Delete(train);
        }

        public async Task<IEnumerable<Train>> GetAllAsync()
        {
            return await _trainDAO.GetAllAsync();
        }

        public Task<IEnumerable<Train>> GetAllByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Train> GetByIdAsync(int id)
        {
            return await _trainDAO.GetByIdAsync(id);
        }

        public Task<IEnumerable<Train>> GetTicketsForARideFilteredByDate(int id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Train train)
        {
            return await _trainDAO.Update(train);
        }
    }
}
