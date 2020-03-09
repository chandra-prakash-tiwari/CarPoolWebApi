﻿using System;

namespace CarPoolingEf.Models
{
    public class Booking
    {
        public string Id { get; set; }

        public string RideId { get; set; }

        public string BookerId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public float TravellingDistance { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime TravelDate { get; set; }

        public BookingState Status { get; set; }
    }

    public class SearchRideRequest
    {
        public string From { get; set; }

        public string To { get; set; }

        public DateTime TravelDate { get; set; }
    }
}
