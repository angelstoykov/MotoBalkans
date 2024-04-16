using Microsoft.AspNetCore.Identity;
using MotoBalkans.Data.Models;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Web.Models.ViewModels
{
    public class MyBookingsViewModel
    {
        public int RentalId { get; set; }

        public Motorcycle Motorcycle { get; set; }

        public ApplicationUser Customer { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
