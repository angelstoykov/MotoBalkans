using Microsoft.AspNetCore.Identity;
using MotoBalkans.Data.Contracts;

namespace MotoBalkans.Data.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsInAdminRole()
        {
            return this.IsInAdminRole();
        }
    }
}
