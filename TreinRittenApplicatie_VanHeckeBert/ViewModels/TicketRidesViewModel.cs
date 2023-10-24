using Microsoft.AspNetCore.Mvc.Rendering;

namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class TicketRidesViewModel
    {
        public IEnumerable<int> RideIds { get; set; }
        public IEnumerable<SelectListItem>? Rides { get; set; }
    }
}
