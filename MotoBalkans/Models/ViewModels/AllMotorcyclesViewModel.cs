namespace MotoBalkans.Web.Models.ViewModels
{
    public class AllMotorcyclesViewModel
    {
        public AllMotorcyclesViewModel(
            int id,
            string brand,
            string model,
            decimal pricePerDay,
            string pictureUrl)
        {
            Id = id;
            Brand = brand;
            Model = model;
            PricePerDay = pricePerDay;
            PictureUrl = pictureUrl;
        }

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal PricePerDay { get; set; }

        public string PictureUrl { get; set; }
    }
}
