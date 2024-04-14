using MotoBalkans.Services.Models;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services.Contracts
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<Motorcycle>> GetAllMotorcycles();

        Task<IEnumerable<Engine>> GetEngineTypes();
        Task<IEnumerable<Transmission>> GetTransmissionTypes();
        Task<Motorcycle> GetMotorcycleById(int id);
        Task<Motorcycle> GetMotorcycleDetailsById(int id);
        Task CreateNewMotorcycle(Motorcycle motorcycle);
        Task<IEnumerable<AvailableMotorcycleDTO>> GetAvailableMotorcyclesForPeriod(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Rental>> GetAllRentals();
        Task DeleteRentals(IEnumerable<Rental> rentalsToDelete);
        Task DeleteMotorcycle(Motorcycle motorcycle);
        Task EditMotorcycle(Motorcycle motorcycle);
        Task<IEnumerable<Motorcycle>> GetPaginatedMotorcycleResult(int currentPage, string sortOrder, int pageSize = 10);
        Task<int> GetCount();
    }
}
