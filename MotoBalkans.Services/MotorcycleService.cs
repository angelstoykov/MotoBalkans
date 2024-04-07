using Microsoft.EntityFrameworkCore;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Enums;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private IMotoBalkansDbContext _data;
        private readonly IRepository<Motorcycle> _motorcycleRepository;
        private readonly IRepository<Engine> _engineRepository;
        private readonly IRepository<Transmission> _transmissionRepository;

        public MotorcycleService(IMotoBalkansDbContext context,
            IRepository<Motorcycle> motorcycleRepository,
            IRepository<Engine> engineRepository,
            IRepository<Transmission> transmissionRepository)
        {
            _data = context;
            _motorcycleRepository = motorcycleRepository;
            _engineRepository = engineRepository;
            _transmissionRepository = transmissionRepository;
        }

        public async Task<IEnumerable<Motorcycle>> GetAllMotorcycles()
        {
            var result = await _data.Motorcycles
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Engine>> GetEngineTypes()
        {
            return await _data.Engines
                .AsNoTracking()
                .ToListAsync();
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
    }
}
