using MotoBalkans.Web.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MotoBalkans.Web.Data.Constants.ValidationMessages;

namespace MotoBalkans.Web.Models.ViewModels
{
    public class AddNewMotocycleFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Brand { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Model { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0.01, 10000.00)]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid price format")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int EngineId { get; set; }

        public IEnumerable<EngineViewModel> EngineTypes { get; set; } = new List<EngineViewModel>();

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int TransmissionId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string PictureUrl { get; set; }

        public IEnumerable<TransmissionViewModel> TransmissionTypes { get; set; } = new List<TransmissionViewModel>();

    }
}
