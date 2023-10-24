using Microsoft.AspNetCore.Mvc.Rendering;

namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class RideTrainsStationsViewModel
    {
        public RideViewModel Ride { get; set; } = null!;
        public IEnumerable<SelectListItem>? Trains { get; set; } = null!;
        public IEnumerable<SelectListItem>? Stations { get; set; } = null!;
    }
}
