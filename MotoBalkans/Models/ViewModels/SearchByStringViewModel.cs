namespace MotoBalkans.Web.Models.ViewModels
{
    public class SearchByStringViewModel : AllMotorcyclesViewModel
    {
        public SearchByStringViewModel(int id, string brand, string model, decimal pricePerDay)
            : base(id,brand, model, pricePerDay) { }
    }
}
