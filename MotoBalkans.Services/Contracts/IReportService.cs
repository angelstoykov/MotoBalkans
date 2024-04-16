using MotoBalkans.Data.Models;
using MotoBalkans.Services.Models;
using MotoBalkans.Services.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Services.Contracts
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAll();

        Task<ReportGetAllRentals> CreateReport(int id);
    }
}
