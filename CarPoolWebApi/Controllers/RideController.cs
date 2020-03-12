using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolWebApi.Controllers
{
    [Route("api/ride/[action]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _RideServices;

        public RideController(IRideService rideServices)
        {
            _RideServices = rideServices;
        }

        [Authorize]
        [HttpPost]
        [ActionName("CreateRide")]
        public IActionResult AddNew([FromBody] Ride ride)
        {
            if (ride == null)
            {
                return NotFound("Ride is null");
            }
            _RideServices.CreateRide(ride);

            return Ok("Created Successful");
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{rideId}")]
        [ActionName("CancelRide")]
        public IActionResult CancelRide(string rideId)
        {
            if (!_RideServices.CancelRide(rideId))
            {
                return BadRequest("Not correct ride id");
            }

            return Ok("Your ride is cancelled");
        }

        [HttpPut("{id}")]
        [ActionName("{Modify}")]
        public IActionResult Update([FromBody] Ride ride,string id)
        {
            if (ride == null)
            {
                return BadRequest("ride is null");
            }
            _RideServices.ModifyRide(ride, id);
            return Ok("Ride id modified");
        }

        [Authorize]
        [HttpGet("{rideId}")]
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

        [Authorize]
        [HttpGet("{ownerId}")]
        [ActionName("YourRide")]
        public IActionResult GetOwnerRides(string ownerId)
        {
            return Ok(_RideServices.GetRides(ownerId));
        }
    }
}