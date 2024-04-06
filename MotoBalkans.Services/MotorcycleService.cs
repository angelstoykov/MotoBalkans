using Microsoft.EntityFrameworkCore;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Contracts;
using MotoBalkans.Web.Data.Enums;
using MotoBalkans.Web.Data.Models;

namespace MotoBalkans.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private IMotoBalkansDbContext _data;

        public MotorcycleService(IMotoBalkansDbContext context)
        {
            _data = context;    
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
            return await _data
                .Transmissions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Motorcycle> GetMotorcycleDetailsById(int id)
        {
            return await _data.Motorcycles
               .AsNoTracking()
               .Where(x => x.Id == id)
               .Include(e => e.Engine)
               .Include(t => t.Transmission)
               .FirstOrDefaultAsync();
        }
    }
}
