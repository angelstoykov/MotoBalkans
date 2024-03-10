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

        [Required]
        public int EngineId { get; set; }

        public IEnumerable<EngineViewModel> EngineTypes { get; set; } = new List<EngineViewModel>();

        [Required]
        public int TransmissionId { get; set; }

        public IEnumerable<TransmissionViewModel> TransmissionTypes { get; set; } = new List<TransmissionViewModel>();

    }
}
