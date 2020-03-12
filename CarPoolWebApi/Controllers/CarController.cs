using CarPoolingWebApi.Models.Client;
using CarPoolingWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolWebApi.Controllers
{
    [Authorize]
    [Route("api/car/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _CarServices;

        public CarController(ICarService carServices)
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
        [ActionName("{remove_car}")]
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