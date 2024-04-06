using Microsoft.EntityFrameworkCore;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Contracts;
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
    }
}
