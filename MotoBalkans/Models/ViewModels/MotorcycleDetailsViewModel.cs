using MotoBalkans.Data.Enums;

namespace MotoBalkans.Web.Models.ViewModels
{
    public class MotorcycleDetailsViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal PricePerDay { get; set; }

        public EngineType EngineType { get; set; }

        public TransmissionType TransmissionType { get; set; }

        public string PictureUrl { get; set; }
    }
}
