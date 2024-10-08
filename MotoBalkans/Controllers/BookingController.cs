﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoBalkans.Data;
using MotoBalkans.Data.Contracts;
using MotoBalkans.Data.Models;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Extentions;
using MotoBalkans.Web.Models.ViewModels;
using System.Security.Claims;

namespace MotoBalkans.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private MotoBalkansDbContext _data;
        private IBookingService _bookingService;
        private IRentalRepository _rentalRepository;
        public BookingController(MotoBalkansDbContext context,
                                 IBookingService bookingService,
                                 IRentalRepository rentalRepository)
        {
            _data = context;
            _bookingService = bookingService;
            _rentalRepository = rentalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Book(AvailableMotorcyclesViewModel availableMotorcycle)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var rental = new Rental();
            var userId = GetUserId();

            rental.CustomerId = userId;
            rental.MotorcycleId = availableMotorcycle.Id;
            rental.StartDate = availableMotorcycle.StartDateRequested;
            rental.EndDate = availableMotorcycle.EndDateRequested;

            await _bookingService.CreateBooking(rental);

            return RedirectToAction("MyBookings", "Booking");
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            string userId = GetUserId();

            var myBookings = await _rentalRepository.GetRentalsForUserAsync(userId);

            var model = myBookings
                .Select(x => new MyBookingsViewModel()
                {
                    Customer = x.Customer,
                    Motorcycle = x.Motorcycle,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    RentalId = x.Id,
                    PictureUrl = x.Motorcycle.PictureUrl
                });

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsInAdminRole() && !User.IsInUserRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            var model = new DeleteMyBookingViewModel()
            {
                Id = booking.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInAdminRole() && !User.IsInUserRole())
            {
                return RedirectToAction("NotAuthorized", "Error");
            }

            var booking = await _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            await _bookingService.DeleteConfirmed(booking);

            return RedirectToAction("MyBookings", "Booking");
        }
    }
}
