using CarPoolingEf;
using CarPoolingEf.Models;
using CarPoolingEf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Services
{
    public class CarServices :  ICarServices
    {
        private CarPoolingContext Db { get; set; }

        public CarServices(CarPoolingContext context) 
        {
            this.Db = context;
        }

        public bool AddNewCar(Car car,string ownerId)
        {
            if (ownerId != null)
            {
                car.Id = Guid.NewGuid().ToString();
                car.OwnerId = ownerId;
                this.Db.Cars.Add(car);
                return this.Db.SaveChanges() > 0;
            }

            return false;
        }

        public bool RemoveCar(string id)
        {
            Car car = this.Db.Cars.FirstOrDefault(a => a.Id == id);
            this.Db.Cars.Remove(car);
            return this.Db.SaveChanges() > 0;
        }

        public List<Car> GetOwnerCars(string id)
        {
            return this.Db?.Cars?.Where(a => a.OwnerId == id).Select(a => a).ToList();
        }

        public Car GetCar(string id)
        {
            return this.Db?.Cars.FirstOrDefault(a => a.Id == id);
        }
    }
}
