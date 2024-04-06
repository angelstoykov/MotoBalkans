using MotoBalkans.Web.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MotoBalkans.Web.Data.Models
{
    public class Transmission
    {
        [Key]
        public int Id { get; set; }

        public TransmissionType TransmissionType { get; set; }
    }
}
