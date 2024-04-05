namespace MotoBalkans.Web.Models.ViewModels
{
    public class AvailableMotorcyclesViewModel
    {
        public AvailableMotorcyclesViewModel()
        {
            
        }

        public AvailableMotorcyclesViewModel(
            int id,
            string brand,
            string model,
            decimal pricePerDay,
            DateTime startDateRequested,
            DateTime endDateRequested)
        {
            Id = id;
            Brand = brand;
            Model = model;
            PricePerDay = pricePerDay;
            StartDateRequested = startDateRequested;
            EndDateRequested = endDateRequested;
        }

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal PricePerDay { get; set; } = 0m;

        public DateTime StartDateRequested { get; set; }

        public DateTime EndDateRequested { get; set; }
    }
}
