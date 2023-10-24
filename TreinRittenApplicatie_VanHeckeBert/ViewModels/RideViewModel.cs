namespace TreinRittenApplicatie_VanHeckeBert.ViewModels
{
    public class RideViewModel
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public string? TrainName { get; set; }
        public string? TrainCapacity { get; set; }
        public string? FromStationCity { get; set; }
        public string? ToStationCity { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Distance { get; set; }
        public IEnumerable<SeatViewModel>? Seats { get; set; }
    }
}
