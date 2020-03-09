using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolWebApi.Controllers
{
    [Route("api/Ride/[Action]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideServices _RideServices;

        public RideController(IRideServices rideServices)
        {
            _RideServices = rideServices;
        }

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

        [HttpGet("{ownerId}")]
        [ActionName("YourRide")]
        public IActionResult GetOwnerRides(string ownerId)
        {
            return Ok(_RideServices.GetRides(ownerId));
        }
    }
}