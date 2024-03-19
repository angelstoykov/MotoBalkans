using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Models.Utilities;
using System;
using System.Collections.Generic;

public class AvailabilityChecker
{
    private List<(DateTime, DateTime)> unavailablePeriods;
    private MotoBalkansDbContext _data;

    public AvailabilityChecker(MotoBalkansDbContext context)
    {
        // Initialize the list of unavailable periods
        unavailablePeriods = new List<(DateTime, DateTime)>();

        // Get all periods from rentals and add them to unavailable periods.
        // Create GetAllRentalPeriods();
        _data = context;
        PopulateUnavailablePeriods();
    }

    private async void PopulateUnavailablePeriods()
    {
        var unavailablePeriodsFromDb = await _data.Rentals
            .Select(p => new UnavailablePeriod()
            {
                StartDate = p.StartDate,
                EndDate = p.EndDate
            })
            .ToListAsync();

        if (unavailablePeriods != null
            && unavailablePeriods.Count > 0)
        {
            foreach (var period in unavailablePeriodsFromDb)
            {
                unavailablePeriods.Add((period.StartDate, period.EndDate));
            }
        }
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
