namespace MotoBalkans.Web.Models.ViewModels
{
    public class AllReportsViewModel
    {
        public List<ReportViewModel> Items { get; set; }

        public AllReportsViewModel()
        {
            Items = new List<ReportViewModel>();
        }
    }
}
