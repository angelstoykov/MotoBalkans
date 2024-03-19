using System;
using System.Collections.Generic;

public class AvailabilityChecker
{
    private List<(DateTime, DateTime)> unavailablePeriods;

    public AvailabilityChecker()
    {
        // Initialize the list of unavailable periods
        unavailablePeriods = new List<(DateTime, DateTime)>();

        // Get all periods from rentals and add them to unavailable periods.
        // Create GetAllRentalPeriods();
    }

    public void AddUnavailablePeriod(DateTime startDate, DateTime endDate)
    {
        unavailablePeriods.Add((startDate, endDate));
    }

    public bool IsAvailable(DateTime requestedStartDate, DateTime requestedEndDate)
    {
        // Check if any unavailable period overlaps with the requested period
        foreach (var (unavailableStart, unavailableEnd) in unavailablePeriods)
        {
            if ((requestedStartDate >= unavailableStart && requestedStartDate <= unavailableEnd) ||
                (requestedEndDate >= unavailableStart && requestedEndDate <= unavailableEnd) ||
                (requestedStartDate <= unavailableStart && requestedEndDate >= unavailableEnd))
            {
                return false;
            }
        }

        return true;
    }
}
