using MotoBalkans.Services.Models;

namespace MotoBalkans.Services.Contracts
{
    public interface IAvailabilityChecker
    {
        void AddUnavailablePeriod(UnavailablePeriod period);

        bool IsAvailable(DateTime requestedStartDate, DateTime requestedEndDate, List<UnavailablePeriod> unavailablePeriods);

        bool IsMotorcycleAvailable(int id, DateTime startDate, DateTime endDate);
    }
}
