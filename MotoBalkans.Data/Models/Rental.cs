using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoBalkans.Data.Models
{
    public class Rental
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int MotorcycleId { get; set; }

        [ForeignKey(nameof(MotorcycleId))]
        public Motorcycle Motorcycle { get; set; } = null!;

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [ForeignKey(nameof(CustomerId))]
        public ApplicationUser Customer { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
