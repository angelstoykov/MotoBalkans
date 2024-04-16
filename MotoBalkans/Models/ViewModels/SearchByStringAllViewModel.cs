namespace MotoBalkans.Web.Models.ViewModels
{
    public class SearchByStringAllViewModel
    {
        public IList<SearchByStringViewModel> Items { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public SearchByStringAllViewModel()
        {
            Items = new List<SearchByStringViewModel>();
        }
    }
}
