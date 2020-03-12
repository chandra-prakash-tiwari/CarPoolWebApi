using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("{bookingid}")]
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
            return Ok(_BookingService.BookingsStatus(ownerId));
        }

        [HttpGet("{id}")]
        [ActionName("{requester}")]
        public IActionResult Requester(string id)
        {
            return Ok(_BookingService.GetRequester(id));
        }

        [HttpGet("{rideId}")]
        [ActionName("{bookings}")]
        public IActionResult GetAllBooker(string rideId)
        {
            return Ok(_BookingService.GetBooking(rideId));
        }

        [HttpGet("{userId}")]
        [ActionName("user_booking")]
        public IActionResult GetUserBooking(string userId)
        {
            return Ok(_BookingService.GetUserBookings(userId));
        }

        [HttpGet("{rideId}")]
        [ActionName("pending_bookings")]
        public IActionResult GetPendingBookings(string rideId)
        {
            return Ok(_BookingService.GetAllPendingBookings(rideId));
        }

        [HttpGet("{bookingId}")]
        [ActionName("booking")]
        public IActionResult GetBooking(string bookigId)
        {
            return Ok(_BookingService.GetBooking(bookigId));
        }
    }
}