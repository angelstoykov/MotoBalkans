using MotoBalkans.Data.Models;

namespace MotoBalkans.Data.Contracts
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetRentalsForUserAsync(string userId);
    }
}
