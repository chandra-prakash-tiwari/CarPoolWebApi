using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolWebApi.Controllers
{
    [Authorize]
    [Route("api/booking/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _BookingService;

        public BookingController(IBookingService bookingService)
        {
            _BookingService = bookingService;
        }

        [HttpPost]
        [ActionName("{create}")]
        public IActionResult CreateBookig([FromBody]Booking booking, string rideId)
        {
            if (booking == null)
            {
                return BadRequest("Booking null is not accepted");
            }
            _BookingService.CreateBooking(booking, rideId);
            return Ok("Booking Created Successful");
        }

        [HttpPut]
        [ActionName("{cancel_booking}")]
        public IActionResult CancelBooking(string id)
        {
            if (!_BookingService.CancelRideRequest(id))
            {
                return BadRequest("No any booking found");
            }

            return Ok(_BookingService.GetBooking(id));
        }

        [HttpGet()]
        [ActionName("{booking}")]
        public IActionResult UserBooking(string ownerId)
        {
            List<Booking> bookings = _BookingService.BookingsStatus(ownerId);

            if (bookings.Any())
            return Ok();

            return NoContent();
        }

        [HttpGet]
        [ActionName("{requester}")]
        public IActionResult Requester(string id)
        {
            string requester = _BookingService.GetRequester(id);
            if (requester == null)
                return NoContent();

            return Ok(requester);
        }

        [HttpGet]
        [ActionName("{viewbookers}")]
        public IActionResult GetAllBookers(string rideId)
        {
            Booking Booking = _BookingService.GetBooking(rideId);
            if (Booking == null)
                return NoContent();

            return Ok(Booking);
        }

        [HttpGet]
        [ActionName("user_booking")]
        public IActionResult GetUserBookings(string userId)
        {
            List<Booking> bookings = _BookingService.GetUserBookings(userId);

            if(bookings.Any())
            return Ok(bookings);

            return NoContent();
        }

        [HttpGet]
        [ActionName("pending_bookings")]
        public IActionResult GetPendingBookings(string rideId)
        {
            List<Booking> bookings = _BookingService.GetPendingBookings(rideId);

            if (bookings.Any())
                return Ok(bookings);

            return NoContent();
        }

        [HttpGet]
        [ActionName("booking")]
        public IActionResult GetBooking(string bookigId)
        {
            Booking booking = _BookingService.GetBooking(bookigId);

            if (booking == null)
                return NoContent();

            return Ok();
        }

        [HttpGet]
        [ActionName("allbookings")]
        public IActionResult GetBooing(string rideId)
        {
            List<Booking> bookings = _BookingService.GetBookings(rideId);

            if (bookings.Any())
                return Ok(bookings);

            return NoContent();
        }
    }
}