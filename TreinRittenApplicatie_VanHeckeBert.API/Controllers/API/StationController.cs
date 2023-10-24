using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinRittenApplicatie_VanHeckeBert.API.ViewModels;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;

namespace TreinRittenApplicatie_VanHeckeBert.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IService<Station> _stationService;

        public StationController(IMapper mapper, IService<Station> stationService)
        {
            _mapper = mapper;
            _stationService = stationService;
        }

        //Get api/Station
        [HttpGet]
        public async Task<IEnumerable<StationViewModel>> Get()
        {
            var stations = await _stationService.GetAllAsync();
            return _mapper.Map<List<StationViewModel>>(stations);
        }
    }
}
