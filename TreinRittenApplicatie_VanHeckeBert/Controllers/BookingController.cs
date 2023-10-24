using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingTicketService _bookingTicketService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IBookingTicketService bookingTicketService, IMapper mapper)
        {
            _bookingService = bookingService;
            _bookingTicketService = bookingTicketService;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string? userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var bookings = await _bookingService.GetAllByIdAsync(userId);
            var bookingViewModels = _mapper.Map<List<BookingViewModel>>(bookings);

            return View(bookingViewModels);
        }

        [Authorize]
        public async Task<IActionResult> CancelBookingTicket(int id, Decimal price)
        {
            var bookingTicket = await _bookingTicketService.GetByIdAsync(id);
            if (bookingTicket == null) return NotFound();
            bookingTicket.Status = false;
            await _bookingTicketService.Update(bookingTicket);

            var bookingTicketsStatus = await _bookingTicketService.GetBookingTicketStatus(bookingTicket.BookingId);

            if(!bookingTicketsStatus.Any())
            {
                var booking = await _bookingService.GetByIdAsync(bookingTicket.BookingId);
                booking.Status = false;
                await _bookingService.Update(booking);
            }

            return RedirectToAction("Index");
        }
    }
}
