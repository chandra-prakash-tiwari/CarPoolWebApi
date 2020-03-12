using CarPoolingWebApi.Context;
using CarPoolingWebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingWebApi.Services.Service
{
    public class CarService : ICarService
    {
        private CarPoolingContext Db { get; set; }

        public CarService(CarPoolingContext context)
        {
            this.Db = context;
        }

        public bool AddNewCar(Models.Client.Car car, string ownerId)
        {
            if (ownerId != null)
            {
                car.Id = Guid.NewGuid().ToString();
                car.OwnerId = ownerId;
                this.Db.Cars.Add(Mapper.Map<Models.Client.Car, Models.Data.Car>(car));
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public bool RemoveCar(string id)
        {
            Models.Data.Car car = this.Db.Cars.FirstOrDefault(a => a.Id == id);
            this.Db.Cars.Remove(car);
            return this.Db.SaveChanges() > 0;
        }

        public List<Models.Client.Car> GetOwnerCars(string id)
        {
            return Mapper.Map<List<Models.Data.Car>, List<Models.Client.Car>>(this.Db.Cars?.Where(a => a.OwnerId == id).Select(a => a).ToList());
        }

        public Models.Client.Car GetCar(string id)
        {
            return Mapper.Map<Models.Data.Car, Models.Client.Car>(this.Db.Cars.FirstOrDefault(a => a.Id == id));
        }
    }
}
