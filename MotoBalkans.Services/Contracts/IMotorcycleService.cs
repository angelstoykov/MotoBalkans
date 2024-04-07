using MotoBalkans.Services.Models;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services.Contracts
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<Motorcycle>> GetAllMotorcycles();

        Task<IEnumerable<Engine>> GetEngineTypes();
        Task<IEnumerable<Transmission>> GetTransmissionTypes();
        Task<Motorcycle> GetMotorcycleDetailsById(int id);
        Task CreateNewMotorcycle(Motorcycle motorcycle);

        Task<IEnumerable<AvailableMotorcycleDTO>> GetAvailableMotorcyclesForPeriod(DateTime startDate, DateTime endDate);
    }
}
