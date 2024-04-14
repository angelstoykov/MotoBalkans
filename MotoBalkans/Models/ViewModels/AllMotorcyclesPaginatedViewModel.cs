namespace MotoBalkans.Web.Models.ViewModels
{
    public class AllMotorcyclesPaginatedViewModel
    {
        public IList<AllMotorcyclesViewModel> Items { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public AllMotorcyclesPaginatedViewModel()
        {
            Items = new List<AllMotorcyclesViewModel>();
        }
    }
}
