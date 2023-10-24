using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;

namespace TreinRittenApplicatie_VanHeckeBert.Service.Interfaces
{
    public interface IBookingService : IService<Booking>
    {
        Task<IEnumerable<Booking>> GetAllByIdAsync(string id);

    }
}
