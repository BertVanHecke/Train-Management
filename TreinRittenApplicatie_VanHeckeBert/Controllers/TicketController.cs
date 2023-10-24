using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TreinRittenApplicatie_VanHeckeBert.Areas.Identity.Data;
using TreinRittenApplicatie_VanHeckeBert.Domain.Data.Enums;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Extensions;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class TicketController : Controller
    {
        private readonly IRideService _rideService;
        private readonly IService<Ticket> _ticketService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TicketController(IRideService rideService, UserManager<ApplicationUser> userManager, IService<Ticket> ticketService, IMapper mapper)
        {
            _rideService = rideService;
            _userManager = userManager;
            _ticketService = ticketService;
            _mapper = mapper;  
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllAsync();
            var ticketViewModels = _mapper.Map<List<TicketViewModel>>(tickets);   
            return View(ticketViewModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id)
        {
            TicketRidesViewModel ticketRidesViewModel = new TicketRidesViewModel();

            var rides = await _rideService.GetAllAsync();
            List<SelectListItem> selectListItems = new();

            foreach (var ride in rides)
            {
                var listItem = new SelectListItem()
                {
                    Text = "From: " + ride.FromStation.City + " at: " + ride.DepartureTime + " - To: " + ride.ToStation.City + " at: " + ride.ArrivalTime,
                    Value = ride.Id.ToString(),
                };
                selectListItems.Add(listItem);
            }

            ticketRidesViewModel.Rides = selectListItems;
            return View(ticketRidesViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TicketRidesViewModel ticketRidesViewModel)
        {
            if (ticketRidesViewModel.RideIds.Count() == 0)
            {
                return View();
            }

            var ticket = new Ticket();
            await _ticketService.Add(ticket);

            Decimal price = 0.00m;

            foreach (var rideId in ticketRidesViewModel.RideIds)
            {
                var ride = await _rideService.GetByIdAsync(rideId);
                ride.Tickets.Add(ticket);
                price += ride.Distance * 0.27m;
            }

            ticket.PriceEconomic = price;
            ticket.PriceBusiness = price * 1.25m;
            await _ticketService.Update(ticket);
            return RedirectToAction("index");
        }
        
        [Authorize]
        public async Task<IActionResult> Select(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _ticketService.GetByIdAsync(id);

            CartViewModel cart = new CartViewModel
            {
                Id = ticket.Id,
                PriceEconomic = ticket.PriceEconomic,
                PriceBusiness = ticket.PriceBusiness,
                DepartureStation = ticket.Rides.ElementAt(0).FromStation.City,
                ArrivalStation = ticket.Rides.ElementAt(ticket.Rides.Count - 1).ToStation.City,
                Date = DateTime.Today,
                ClassType = 1 
            };

            ShoppingCartViewModel? shopping;

            if (HttpContext.Session.GetObject<ShoppingCartViewModel>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartViewModel>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartViewModel();
                shopping.Cart = new List<CartViewModel>();
            }
            shopping.Cart.Add(cart);


            HttpContext.Session.SetObject("ShoppingCart", shopping);


            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
