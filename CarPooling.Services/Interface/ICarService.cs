using CarPoolingWebApi.Models.Client;
using System.Collections.Generic;

namespace CarPoolingWebApi.Services.Interfaces
{
    public interface ICarService
    {
        bool AddNewCar(Car car,string ownerId);

        bool RemoveCar(string id);

        List<Car> GetOwnerCars(string id);

        Car GetCar(string id);
    }
}
