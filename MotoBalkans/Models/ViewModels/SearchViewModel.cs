using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MotoBalkans.Web.Models.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddDays(1);
        }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
