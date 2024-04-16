using MotoBalkans.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Services.Models.Reports
{
    public class ReportGetAllRentals
    {
        public List<Rental> Items { get; set; }

        public ReportGetAllRentals()
        {
            Items = new List<Rental>();
        }

        public int ReportId { get; set; }
    }
}
