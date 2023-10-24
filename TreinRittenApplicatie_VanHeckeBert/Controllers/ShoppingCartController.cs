using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TreinRittenApplicatie_VanHeckeBert.Areas.Identity.Data;
using TreinRittenApplicatie_VanHeckeBert.Domain.Entities;
using TreinRittenApplicatie_VanHeckeBert.Extensions;
using TreinRittenApplicatie_VanHeckeBert.Service.Interfaces;
using TreinRittenApplicatie_VanHeckeBert.Util.Mail;
using TreinRittenApplicatie_VanHeckeBert.ViewModels;

namespace TreinRittenApplicatie_VanHeckeBert.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingTicketService _bookingTicketService;
        private readonly IService<Ticket> _ticketService;
        private readonly IService<Seat> _seatService;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IBookingService bookingService, IBookingTicketService bookingTicketService, IService<Ticket> ticketService, IService<Seat> seatService, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _bookingTicketService = bookingTicketService;
            _ticketService = ticketService;
            _seatService = seatService;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            ShoppingCartViewModel? cartList = HttpContext.Session.GetObject<ShoppingCartViewModel>("ShoppingCart");
            return View(cartList);
        }

        [Authorize]
        public IActionResult Delete(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }
            ShoppingCartViewModel? cartList
              = HttpContext.Session
              .GetObject<ShoppingCartViewModel>("ShoppingCart");

            CartViewModel? itemToRemove =
                cartList?.Cart?.FirstOrDefault(r => r.Id == Id);
            // db.bieren.FirstOrDefault (r => 

            if (itemToRemove != null)
            {
                cartList?.Cart?.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }

            if (cartList.Cart.Count == 0 || cartList == null)
            {
                return View("index", null);
            }
            else
            {
                return View("index", cartList);
            }
        }

        [Authorize]
        public async Task<IActionResult> Payment(ShoppingCartViewModel carts)
        {
            string? userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var bookings = await _bookingService.GetAllAsync();

            try
            {
                var booking = new Booking
                {
                    Id = bookings.Count(),
                    AspNetUserId = userId,
                    Status = true,
                };

                await _bookingService.Add(booking);

                foreach (var ticket in carts.Cart)
                {
                    var bookingTickts = await _bookingTicketService.GetAllAsync();
                    var seatsForARide = await _bookingTicketService.GetTicketsForARideFilteredByDate(ticket.Id, ticket.Date);

                    var bookingTicket = new BookingTicket
                    {
                        Id = bookingTickts.Count(),
                        TicketId = ticket.Id,
                        Date = ticket.Date,
                        Class = ticket.ClassType,
                        BookingId = booking.Id,
                        Status = true
                    };
                    await _bookingTicketService.Add(bookingTicket);

                    var ticketRides = await _ticketService.GetByIdAsync(ticket.Id);

                    foreach (var ride in ticketRides.Rides)
                    {

                        if (seatsForARide.Count() > ride.Train.Capacity)
                        {
                            await _bookingService.Delete(booking);
                            await _bookingTicketService.Delete(bookingTicket);
                            ViewBag.Error = "The ticket: " + ticketRides.Rides.ElementAt(0).FromStation.City + " " + ticketRides.Rides.ElementAt(ticketRides.Rides.Count - 1).ToStation.City + " is sold out for the chosen date: " + ticket.Date;
                            return RedirectToAction("Index");
                        }
                        var seats = await _seatService.GetAllAsync();
                        var seat = new Seat
                        {
                            Id = seats.Count(),
                            RideId = ride.Id,
                            BookingTicketId = bookingTicket.Id,
                            Number = seatsForARide.Count() + 1,
                        };
                        await _seatService.Add(seat);
                        await _emailSender.SendEmailAsync(user.Email, "Hello " + user.FirstName + ", Your booking has been confirmed.", "<p>Booking Id: " + booking.Id + "</p>");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Payment: " + ex.Message);
            }

            return RedirectToAction("Index", "Ticket");
        }
    }
}
