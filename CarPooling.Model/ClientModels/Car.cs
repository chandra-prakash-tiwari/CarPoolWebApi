﻿
using System.ComponentModel.DataAnnotations;

namespace CarPoolingWebApi.Models.Client
{
    public class Car
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter car number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Please enter car model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter max number of seats")]
        public int NoofSeat { get; set; }

        public string OwnerId { get; set; }
    }
}
