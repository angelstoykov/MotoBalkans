using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Models;

public class RentalRepository : IRentalRepository
{
    private readonly IMotoBalkansDbContext _data;

    public RentalRepository(IMotoBalkansDbContext data)
    {
        _data = data;
    }

    public async Task<List<Rental>> GetRentalsForUserAsync(string userId)
    {
        return await _data.Rentals
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Motorcycle)
            .Where(sp => sp.CustomerId == userId)
            .ToListAsync();
    }
}
