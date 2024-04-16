﻿using Microsoft.AspNetCore.Identity;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Models;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Services.Models.Reports;
using MotoBalkans.Web.Data.Models;
using System.Security.Claims;

namespace MotoBalkans.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _reportsRepository;
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IMotorcycleService _motorcycleService;
        private readonly UserManager<IdentityUser> _userManager;


        public ReportService(
            IRepository<Report> reportRepository,
            IRepository<Rental> rentalRepository,
            IMotorcycleService motorcycleService,
            UserManager<IdentityUser> userManager)
        {
            _reportsRepository = reportRepository;
            _rentalRepository = rentalRepository;
            _motorcycleService = motorcycleService;
            _userManager = userManager;
        }

        public async Task<ReportGetAllRentals> CreateReport(int id)
        {
            switch (id)
            {
                case 1:
                    var result = await CreateGetAllRentalsReport();
                    result.ReportId = id;

                    return result;
            }

            return null;
        }

        private async Task<ReportGetAllRentals> CreateGetAllRentalsReport()
        {
            var allRentals = await _rentalRepository.GetAll();

            var reportGetAllRentals = new ReportGetAllRentals();

            

            foreach (var item in allRentals)
            {
                var motorcycle = await _motorcycleService.GetMotorcycleById(item.MotorcycleId);

                item.Motorcycle = motorcycle;

                var user = await _userManager.FindByIdAsync(item.CustomerId);

                item.Customer = user;
                
                reportGetAllRentals.Items.Add(item);
            }

            return reportGetAllRentals;
        }

        public async Task<IEnumerable<Report>> GetAll()
        {
            var reports = await _reportsRepository.GetAll();

            return reports;
        }


    }
}