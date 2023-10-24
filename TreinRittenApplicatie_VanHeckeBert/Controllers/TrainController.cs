using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class TrainController : Controller
    {
        private readonly IService<Train> _trainService;
        private readonly IMapper _mapper;

        public TrainController(IService<Train> trainService, IMapper mapper)
        {
            _trainService = trainService;
            _mapper = mapper;   
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var trains = await _trainService.GetAllAsync();
            var trainViewModels = _mapper.Map<List<TrainViewModel>>(trains);
            return View(trainViewModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var trainViewModel = new TrainViewModel();
            return View(trainViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TrainViewModel trainViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(trainViewModel);
            }

            var train = _mapper.Map<Train>(trainViewModel);
            await _trainService.Add(train);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var train = await _trainService.GetByIdAsync(id);
            if (train == null) return NotFound();

            var trainViewModel = _mapper.Map<TrainViewModel>(train);

            return View(trainViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, TrainViewModel trainViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Failed to edit train");
                return View("Edit", trainViewModel);
            }

            var train = _mapper.Map<Train>(trainViewModel);
            train.Id = id;

            await _trainService.Update(train);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var train = await _trainService.GetByIdAsync(id);
            if (train == null) return View("Error");
            await _trainService.Delete(train);
            return RedirectToAction("Index");
        }
    }
}
