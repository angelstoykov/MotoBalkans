using Microsoft.AspNetCore.Localization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoBalkans.Web.Data.Models
{
    public class Rental
    {
        [Required]
        public int MotorcycleId { get; set; }

        [ForeignKey(nameof(MotorcycleId))]
        public Motorcycle Motorcycle { get; set; } = null!;

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
