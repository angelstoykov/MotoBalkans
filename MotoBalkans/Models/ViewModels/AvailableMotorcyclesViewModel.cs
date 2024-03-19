namespace MotoBalkans.Web.Models.ViewModels
{
    public class AvailableMotorcyclesViewModel
    {
        public AvailableMotorcyclesViewModel(
            int id,
            string brand,
            string model,
            decimal pricePerDay)
        {
            Id = id;
            Brand = brand;
            Model = model;
            PricePerDay = pricePerDay;
        }

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal PricePerDay { get; set; } = 0m;
    }
}
