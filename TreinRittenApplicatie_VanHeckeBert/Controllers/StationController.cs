using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class StationController : Controller
    {
        private readonly IService<Station> _stationService;
        private readonly IMapper _mapper;

        public StationController(IService<Station> stationService, IMapper mapper)
        {
            _stationService = stationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var stations = await _stationService.GetAllAsync();
            var stationViewModels = _mapper.Map<List<StationViewModel>>(stations);
            return View(stationViewModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var stationViewModel = new StationViewModel();
            return View(stationViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(StationViewModel stationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(stationViewModel);
            }

            var station = _mapper.Map<Station>(stationViewModel);
            await _stationService.Add(station);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var station = await _stationService.GetByIdAsync(id);
            if (station == null) return View("Error");
            await _stationService.Delete(station);
            return RedirectToAction("Index");
        }
    }
}
