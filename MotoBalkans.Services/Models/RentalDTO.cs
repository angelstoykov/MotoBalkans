using MotoBalkans.Data.Models;

namespace MotoBalkans.Services.Models
{
    public class RentalDTO : Rental
    {
        public decimal TotalPrice { get; set; }
    }
}
