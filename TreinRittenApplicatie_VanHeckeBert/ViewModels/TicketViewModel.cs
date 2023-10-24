namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public decimal PriceEconomic { get; set; }
        public decimal PriceBusiness { get; set; }
        public IEnumerable<RideViewModel> Rides { get; set; }
    }
}
