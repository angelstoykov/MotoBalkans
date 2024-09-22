namespace MotoBalkans.Services.Models.Reports
{
    public class ReportGetAllRentals
    {
        public List<RentalDTO> Items { get; set; }

        public ReportGetAllRentals()
        {
            Items = new List<RentalDTO>();
        }

        public int ReportId { get; set; }

        public decimal GetGrandTotal()
        {
           return Items.Sum(r => r.TotalPrice); ;
        }
    }
}
