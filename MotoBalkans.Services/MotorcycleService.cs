using MotoBalkans.Data.Contracts;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Services.Models;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IRepository<Motorcycle> _motorcycleRepository;
        private readonly IRepository<Engine> _engineRepository;
        private readonly IRepository<Transmission> _transmissionRepository;
        private IAvailabilityChecker _checker;

        public MotorcycleService(IMotoBalkansDbContext context,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Engine> engineRepository,
            IRepository<Transmission> transmissionRepository,
            IAvailabilityChecker checker)
        {
            _motorcycleRepository = motorcycleRepository;
            _engineRepository = engineRepository;
            _transmissionRepository = transmissionRepository;
            _checker = checker;
        }

        public async Task<IEnumerable<Motorcycle>> GetAllMotorcycles()
        {
            return await _motorcycleRepository.GetAll();
        }

        public async Task<IEnumerable<Engine>> GetEngineTypes()
        {
            return await _engineRepository.GetAll();
        }

        public async Task<IEnumerable<Transmission>> GetTransmissionTypes()
        {
            return await _transmissionRepository.GetAll();
        }

        public async Task<Motorcycle> GetMotorcycleDetailsById(int id)
        {
            var motorcycle = await _motorcycleRepository.GetById(id);

            var engine = await _engineRepository.GetById(motorcycle.EngineId);
            motorcycle.Engine = engine;

            var transmission = await _transmissionRepository.GetById(motorcycle.TransmissionId);
            motorcycle.Transmission = transmission;

            return motorcycle;
        }

        public async Task CreateNewMotorcycle(Motorcycle motorcycle)
        {
            _motorcycleRepository.Add(motorcycle);
        }

        public async Task<IEnumerable<AvailableMotorcycleDTO>> GetAvailableMotorcyclesForPeriod(DateTime startDate, DateTime endDate)
        {
            // TODO: This method is not finished yet.
            var allMotorcycles = await _motorcycleRepository.GetAll();

            var availableMotorcycles = new List<AvailableMotorcycleDTO>();

            foreach (var motorcycle in allMotorcycles)
            {
                var isMotocycleAvailable = _checker.IsMotorcycleAvailable(motorcycle.Id,
                                                                          startDate,
                                                                          endDate);

                if (isMotocycleAvailable)
                {
                    availableMotorcycles.Add(new AvailableMotorcycleDTO(motorcycle.Id,
                                                                        motorcycle.Model,
                                                                        motorcycle.Brand,
                                                                        0,
                                                                        startDate,
                                                                        endDate));
                }
            }

            return availableMotorcycles;
        }

        public async Task<Motorcycle> GetMotorcycleById(int id)
        {
            return await GetMotorcycleDetailsById(id);
        }
    }
}
