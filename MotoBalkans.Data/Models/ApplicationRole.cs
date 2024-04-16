using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Data.Models
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string role) : base(role)
        {
           
        }
    }
}
