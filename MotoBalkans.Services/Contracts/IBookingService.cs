using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services.Contracts
{
    public interface IBookingService
    {
        Task CreateBooking(Rental rental);

        Task<Rental> GetBookingById(int id);

        Task DeleteConfirmed(Rental booking);
    }
}
