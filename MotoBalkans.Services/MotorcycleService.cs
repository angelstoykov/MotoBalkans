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
        private readonly IRepository<Rental> _rentalRepository;
        private IAvailabilityChecker _checker;

        public MotorcycleService(IMotoBalkansDbContext context,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Engine> engineRepository,
            IRepository<Transmission> transmissionRepository,
            IRepository<Rental> rentalRepository,
            IAvailabilityChecker checker)
        {
            _motorcycleRepository = motorcycleRepository;
            _engineRepository = engineRepository;
            _transmissionRepository = transmissionRepository;
            _rentalRepository = rentalRepository;
            _checker = checker;
        }

        public async Task<IEnumerable<Motorcycle>> GetPaginatedMotorcycleResult(int currentPage, string sortOrder, int pageSize = 10)
        {
            var allMotorcyclesFromDb = await GetAllMotorcycles();
            switch (sortOrder)
            {
                case "brand_name_desc":
                    allMotorcyclesFromDb = allMotorcyclesFromDb.OrderByDescending(s => s.Brand);
                    break;
                case "model_name_desc":
                    allMotorcyclesFromDb = allMotorcyclesFromDb.OrderByDescending(s => s.Model);
                    break;
                case "model_name_asc":
                    allMotorcyclesFromDb = allMotorcyclesFromDb.OrderBy(s => s.Model);
                    break;
                default:
                    allMotorcyclesFromDb = allMotorcyclesFromDb.OrderBy(s => s.Brand);
                    break;
            }

            return allMotorcyclesFromDb.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await GetAllMotorcycles();
            return data.ToList().Count;
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

            if (motorcycle != null)
            {
                var engine = await _engineRepository.GetById(motorcycle.EngineId);
                motorcycle.Engine = engine;

                var transmission = await _transmissionRepository.GetById(motorcycle.TransmissionId);
                motorcycle.Transmission = transmission;
            }

            return motorcycle;
        }

        public async Task CreateNewMotorcycle(Motorcycle motorcycle)
        {
            await _motorcycleRepository.Add(motorcycle);
        }

        public async Task<IEnumerable<AvailableMotorcycleDTO>> GetAvailableMotorcyclesForPeriod(DateTime startDate, DateTime endDate)
        {
            var allMotorcycles = await _motorcycleRepository.GetAll();

            var availableMotorcycles = new List<AvailableMotorcycleDTO>();

            foreach (var motorcycle in allMotorcycles)
            {
                var isMotocycleAvailable = _checker.IsMotorcycleAvailable(motorcycle.Id,
                                                                          startDate,
                                                                          endDate);

                if (isMotocycleAvailable)
                {
                    var priceForThePeriod = Math.Round(((int)(endDate - startDate).TotalDays * motorcycle.PricePerDay), 2);
                    availableMotorcycles.Add(new AvailableMotorcycleDTO(motorcycle.Id,
                                                                        motorcycle.Brand,
                                                                        motorcycle.Model,
                                                                        0,
                                                                        startDate,
                                                                        endDate,
                                                                        priceForThePeriod,
                                                                        motorcycle.PictureUrl));
                }
            }

            return availableMotorcycles;
        }

        public async Task<Motorcycle> GetMotorcycleById(int id)
        {
            return await GetMotorcycleDetailsById(id);
        }

        public async Task<IEnumerable<Rental>> GetAllRentals()
        {
            return await _rentalRepository.GetAll();
        }

        public async Task DeleteRentals(IEnumerable<Rental> rentalsToDelete)
        {

            _rentalRepository.DeleteRange(rentalsToDelete);
        }

        public async Task DeleteMotorcycle(Motorcycle motorcycle)
        {

            await _motorcycleRepository.Delete(motorcycle);
        }

        public async Task EditMotorcycle(Motorcycle motorcycle)
        {
            _motorcycleRepository.Update(motorcycle);
        }
    }
}
