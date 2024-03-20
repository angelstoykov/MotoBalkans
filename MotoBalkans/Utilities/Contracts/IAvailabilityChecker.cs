using MotoBalkans.Web.Models.Utilities;

namespace MotoBalkans.Web.Utilities.Contracts
{
    public interface IAvailabilityChecker
    {
        void AddUnavailablePeriod(UnavailablePeriod period);

        bool IsAvailable(DateTime requestedStartDate, DateTime requestedEndDate, List<UnavailablePeriod> unavailablePeriods);

        bool IsMotorcycleAvailable(int id, DateTime startDate, DateTime endDate);
    }
}
