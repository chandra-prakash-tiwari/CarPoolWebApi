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
    [Route("api/Car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _CarServices;

        public CarController(ICarServices carServices)
        {
            _CarServices = carServices;
        }

        [HttpPost("{ownerId}")]
        [ActionName("{add_new_car}")]
        public IActionResult NewCar([FromBody] Car car,string ownerId)
        {
            if (car == null)
            {
                return BadRequest();
            }
            _CarServices.AddNewCar(car,ownerId);
            return Ok("Car adding successful");
        }

        [HttpDelete("{carId}")]
        [ActionName("{Remove_car}")]
        public IActionResult Remove(string id)
        {
            if (!_CarServices.RemoveCar(id))
                return BadRequest();
            return Ok();
        }

        [HttpGet("{ownerId}")]
        [ActionName("{cars}")]
        public IActionResult GetCars(string ownerId)
        {
            return Ok(_CarServices.GetOwnerCars(ownerId));
        }

        [HttpGet("{carId}")]
        [ActionName("{car}")]
        public IActionResult Car(string carId)
        {
            return Ok(_CarServices.GetCar(carId));
        }
    }
}