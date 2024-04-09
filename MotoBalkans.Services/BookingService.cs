using MotoBalkans.Data.Contracts;
using MotoBalkans.Services.Contracts;
using MotoBalkans.Web.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoBalkans.Services
{
    public class BookingService : IBookingService
    {
        private IRepository<Rental> _rentalRepository;
        public BookingService(IRepository<Rental> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task CreateBooking(Rental rental)
        {
            await _rentalRepository.Add(rental);
        }

        public async Task<Rental> GetBookingById(int id)
        {
            return await _rentalRepository.GetById(id);
        }

        public async Task DeleteConfirmed(Rental booking)
        {
            await _rentalRepository.Delete(booking);
        }
    }
}
