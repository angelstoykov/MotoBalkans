namespace MotoBalkans.Services.Models;

public class UnavailablePeriod
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MotorcycleId { get; set; }
}
