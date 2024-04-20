using MotoBalkans.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MotoBalkans.Data.Models
{
    public class Transmission
    {
        [Key]
        public int Id { get; set; }

        public TransmissionType TransmissionType { get; set; }
    }
}
