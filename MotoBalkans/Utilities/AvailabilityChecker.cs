using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Models.Utilities;
using MotoBalkans.Web.Utilities.Contracts;
using System;
using System.Collections.Generic;

public class AvailabilityChecker : IAvailabilityChecker
{
    private List<UnavailablePeriod> unavailablePeriods;
    private IMotoBalkansDbContext _data;

    public AvailabilityChecker(IMotoBalkansDbContext context)
    {
        // Initialize the list of unavailable periods
        unavailablePeriods = new List<UnavailablePeriod>();

        // Get all periods from rentals and add them to unavailable periods.
        // Create GetAllRentalPeriods();
        _data = context;
        PopulateUnavailablePeriods();
    }

    private void PopulateUnavailablePeriods()
    {
        var unavailablePeriodsFromDb = _data.Rentals
            .Select(p => new UnavailablePeriod()
            {
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                MotorcycleId = p.MotorcycleId
            })
            .ToList();

        if (unavailablePeriodsFromDb != null
            && unavailablePeriodsFromDb.Count > 0)
        {
            foreach (var period in unavailablePeriodsFromDb)
            {
                unavailablePeriods.Add(period);
            }
        }
    }

    public void AddUnavailablePeriod(UnavailablePeriod period)
    {
        unavailablePeriods.Add(period);
    }

    public bool IsMotorcycleAvailable(int motorcycleId, DateTime startDate, DateTime endDate)
    {
        var unavailablePeriodsForMotorcycle = unavailablePeriods.Where(x => x.MotorcycleId == motorcycleId).ToList();
        var isAvailable = IsAvailable(startDate, endDate, unavailablePeriodsForMotorcycle);

        return isAvailable;
    }

    public bool IsAvailable(DateTime requestedStartDate, DateTime requestedEndDate, List<UnavailablePeriod> unavailablePeriods)
    {
        // Check if any unavailable period overlaps with the requested period
        foreach (var unavailablePeriod in unavailablePeriods)
        {
            if ((requestedStartDate >= unavailablePeriod.StartDate && requestedStartDate <= unavailablePeriod.EndDate)
                || (requestedEndDate >= unavailablePeriod.StartDate && requestedEndDate <= unavailablePeriod.EndDate)
                || (requestedStartDate <= unavailablePeriod.StartDate && requestedEndDate >= unavailablePeriod.EndDate))
            {
                return false;
            }
        }

        return true;
    }
}
