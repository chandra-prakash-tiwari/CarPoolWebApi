﻿using System.Collections.Generic;

namespace CarPoolingWebApi.Models.Data
{
    public class Car
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public string Model { get; set; }

        public int NoofSeat { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Ride> Rides { get; set; }
    }
}
