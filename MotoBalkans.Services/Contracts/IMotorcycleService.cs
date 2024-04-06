using MotoBalkans.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Services.Contracts
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<Motorcycle>> GetAllMotorcycles();

        Task<IEnumerable<Engine>> GetEngineTypes();
    }
}
