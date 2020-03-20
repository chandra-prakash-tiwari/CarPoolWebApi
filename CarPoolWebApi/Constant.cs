using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPoolWebApi
{
    public class Constant
    {
        public static readonly string UserNotFound ="User record couldn't found";

        public static readonly string NullUser ="User is null ";

        public static readonly string UserNameNotAvailable = "This username taken by some use another one";
        
        public static readonly string EmptyOwnerId = "Owner id is empty";

        public static readonly string CarAdded = "Car adding successful";

        public static readonly string RideCreated = "Created Successful";

        public static readonly string RideCancelled = "Your ride is cancelled";

        public static readonly string RideModified = "Ride id modified";

        public static readonly string NoRide = "No ride found";

        public static readonly string NullBooking = "Booking null is not accepted";

        public static readonly string BookingCreated = "Booking created successful";

        public static readonly string BookingNotFound ="Booking not found";


    }
}
