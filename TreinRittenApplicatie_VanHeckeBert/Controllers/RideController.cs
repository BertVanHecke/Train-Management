using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class RideController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IService<Train> _trainService;
        private readonly IService<Station> _stationService;
        private readonly IMapper _mapper;


        public RideController(IRideService rideService, IService<Train> trainService, IService<Station> stationService, IMapper mapper)
        {
            _rideService = rideService;
            _trainService = trainService;
            _stationService = stationService;
            _mapper = mapper;   
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var rides = await _rideService.GetAllAsync();
            var rideViewModels = _mapper.Map<List<RideViewModel>>(rides);
            return View(rideViewModels);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create()
        {
            RideTrainsStationsViewModel rideTrainsStationsViewModel = new RideTrainsStationsViewModel();

            rideTrainsStationsViewModel.Trains = new SelectList(await _trainService.GetAllAsync(), "Id", "Name");
            rideTrainsStationsViewModel.Stations = new SelectList(await _stationService.GetAllAsync(), "Id", "City");

            return View(rideTrainsStationsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(RideTrainsStationsViewModel rideTrainsStationsViewModel)
        {
            if (!ModelState.IsValid)
            {
                rideTrainsStationsViewModel.Trains = new SelectList(await _trainService.GetAllAsync(), "Id", "Name");
                rideTrainsStationsViewModel.Stations = new SelectList(await _stationService.GetAllAsync(), "Id", "City");

                return View(rideTrainsStationsViewModel);
            }
            var ride = _mapper.Map<Ride>(rideTrainsStationsViewModel.Ride);
            await _rideService.Add(ride);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id)
        {
            var ride = await _rideService.GetByIdAsync(id);
            if (ride == null) return NotFound();

            RideTrainsStationsViewModel rideTrainsStationsViewModel = new RideTrainsStationsViewModel();
            rideTrainsStationsViewModel.Trains = new SelectList(await _trainService.GetAllAsync(), "Id", "Name");
            rideTrainsStationsViewModel.Stations = new SelectList(await _stationService.GetAllAsync(), "Id", "City");
            rideTrainsStationsViewModel.Ride = _mapper.Map<RideViewModel>(ride);

            return View(rideTrainsStationsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, RideTrainsStationsViewModel rideTrainsStationsViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Failed to edit ride");

                rideTrainsStationsViewModel.Trains = new SelectList(await _trainService.GetAllAsync(), "Id", "Name");
                rideTrainsStationsViewModel.Stations = new SelectList(await _stationService.GetAllAsync(), "Id", "City");

                return View("Edit", rideTrainsStationsViewModel);
            }

            var ride = _mapper.Map<Ride>(rideTrainsStationsViewModel.Ride);
            ride.Id = id;
            await _rideService.Update(ride);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ride = await _rideService.GetByIdAsync(id);
            if (ride == null) return View("Error");
            await _rideService.Delete(ride);
            return RedirectToAction("Index");
        }
    }
}
