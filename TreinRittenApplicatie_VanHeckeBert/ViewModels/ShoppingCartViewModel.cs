namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartViewModel>? Cart { get; set; }
    }

    public class CartViewModel
    {
        public int Id { get; set; }
        public Decimal PriceEconomic { get; set; }
        public Decimal PriceBusiness { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public int ClassType { get; set; }
        public DateTime Date { get; set; }
    }
}
