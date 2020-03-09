﻿using CarPoolingEf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingEf.Services.Interfaces
{
    public interface ICarServices
    {
        bool AddNewCar(Car car,string ownerId);

        bool RemoveCar(string id);

        List<Car> GetOwnerCars(string id);

        Car GetCar(string id);
    }
}
