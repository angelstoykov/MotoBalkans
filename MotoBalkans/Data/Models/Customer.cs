using System.ComponentModel.DataAnnotations;

namespace MotoBalkans.Web.Data.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public IList<Rental> Rentals { get; set; } = new List<Rental>();
    }
}