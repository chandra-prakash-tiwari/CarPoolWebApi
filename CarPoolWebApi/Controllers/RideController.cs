 using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolWebApi.Controllers
{
    [Authorize]
    [Route("api/ride/[action]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _RideServices;

        public RideController(IRideService rideServices)
        {
            _RideServices = rideServices;
        }

        [HttpPost]
        [ActionName("createride")]
        public IActionResult AddNew([FromBody] Ride ride)
        {
            if (ride == null)
            {
                return NoContent();
            }
            _RideServices.CreateRide(ride);

            return Ok(Constant.RideCreated);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut]
        [ActionName("cancelride")]
        public IActionResult CancelRide(string rideId)
        {
            if (!_RideServices.CancelRide(rideId))
            {
                return NoContent();
            }

            return Ok(Constant.RideCancelled);
        }

        [HttpPut]
        [ActionName("modify")]
        public IActionResult Update([FromBody] Ride ride,string id)
        {
            if (ride == null)
            {
                return NoContent();
            }
            _RideServices.ModifyRide(ride, id);
            return Ok(Constant.RideModified);
        }

        [HttpGet]
        [ActionName("ride")]
        public IActionResult GetRide(string rideId)
        {
            Ride ride = _RideServices.GetRide(rideId);
            if (ride == null)
            {
                return NotFound(Constant.NoRide);
            }

            return Ok(ride);
        }

        [HttpGet]
        [ActionName("yourride")]
        public IActionResult GetOwnerRides(string ownerId)
        {
            return Ok(_RideServices.GetRides(ownerId));
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("searchride")]
        public IActionResult GetRidesOffers(SearchRideRequest booking)
        {
            return Ok(_RideServices.GetRidesOffers(booking));
        }

    }
}