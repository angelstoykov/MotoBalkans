using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotoBalkans.Web.Data.Models
{
    public class Motorcycle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int EngineId { get; set; }

        [Required]
        [ForeignKey(nameof(EngineId))]
        public Engine Engine { get; set; } = null!;

        [Required]
        public int TransmissionId { get; set; }

        [Required]
        [ForeignKey(nameof(TransmissionId))]
        public Transmission Transmission { get; set; } = null!;

        public IList<Rental> Customers { get; set; } = new List<Rental>();

        [Required]
        [Precision(18,2)]
        public decimal PricePerDay { get; set; }
    }
}
