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
    public class StationService : IService<Station>
    {

        private readonly IDAO<Station> _stationDAO;

        public StationService(IDAO<Station> stationDAO) 
        {
            _stationDAO = stationDAO;
        }

        public async Task<bool> Add(Station station)
        {
            return await _stationDAO.Add(station);
        }

        public async Task<bool> Delete(Station station)
        {
            return await _stationDAO.Delete(station);
        }

        public async Task<IEnumerable<Station>> GetAllAsync()
        {
            return await _stationDAO.GetAllAsync();
        }

        public Task<IEnumerable<Station>> GetAllByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Station> GetByIdAsync(int id)
        {
            return await _stationDAO.GetByIdAsync(id);
        }

        public Task<IEnumerable<Station>> GetTicketsForARideFilteredByDate(int id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Station station)
        {
            return await _stationDAO.Update(station);
        }
    }
}
