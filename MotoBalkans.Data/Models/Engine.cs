using MotoBalkans.Web.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MotoBalkans.Web.Data.Models
{
    public class Engine
    {
        [Key]
        public int Id { get; set; }

        public EngineType EngineType { get; set; }

        public int Size { get; set; }

        public int Consumption { get; set; }
    }
}