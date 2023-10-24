using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinRittenApplicatie_VanHeckeBert.API.ViewModels;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;

namespace TreinRittenApplicatie_VanHeckeBert.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRideService _rideService;
        public TrainController(IMapper mapper, IRideService rideService)
        {
            _mapper = mapper;
            _rideService = rideService;
        }

        [HttpGet("{fromStationId}/{toStationId}", Name = "Get")]
        public async Task<TrainViewModel> Get(int fromStationId, int toStationId)
        {
            var ride = await _rideService.GetTrainForStartAndStopStation(fromStationId, toStationId);
            return _mapper.Map<TrainViewModel>(ride);
        }
    }
}
