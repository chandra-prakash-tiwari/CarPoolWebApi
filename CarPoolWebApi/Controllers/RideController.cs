using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [ActionName("CreateRide")]
        public IActionResult AddNew([FromBody] Ride ride)
        {
            if (ride == null)
            {
                return NoContent();
            }
            _RideServices.CreateRide(ride);

            return Ok("Created Successful");
        }

        [Authorize(Roles ="Admin")]
        [HttpPut]
        [ActionName("CancelRide")]
        public IActionResult CancelRide(string rideId)
        {
            if (!_RideServices.CancelRide(rideId))
            {
                return NoContent();
            }

            return Ok("Your ride is cancelled");
        }

        [HttpPut]
        [ActionName("{Modify}")]
        public IActionResult Update([FromBody] Ride ride,string id)
        {
            if (ride == null)
            {
                return NoContent();
            }
            _RideServices.ModifyRide(ride, id);
            return Ok("Ride id modified");
        }

        [HttpGet]
        [ActionName("Ride")]
        public IActionResult GetRide(string rideId)
        {
            Ride ride = _RideServices.GetRide(rideId);
            if (ride == null)
            {
                return NotFound("No ride found");
            }

            return Ok(ride);
        }

        [HttpGet]
        [ActionName("YourRide")]
        public IActionResult GetOwnerRides(string ownerId)
        {
            return Ok(_RideServices.GetRides(ownerId));
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("searchride")]
        public IActionResult GetRidesOffers(SearchRideRequest booking)
        {
            List<Ride> rides = _RideServices.GetRidesOffers(booking);
            if (rides == null)
                return NoContent();

            return Ok(rides);
        }

    }
}