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

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [GreaterThan(nameof(StartDate), ErrorMessage = "End Date must be greater than Start Date.")]
        public DateTime EndDate { get; set; }
    }
}
