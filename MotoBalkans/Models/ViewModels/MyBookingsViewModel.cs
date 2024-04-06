using Microsoft.AspNetCore.Identity;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Web.Models.ViewModels
{
    public class MyBookingsViewModel
    {
        public Motorcycle Motorcycle { get; set; }

        public IdentityUser Customer { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
