using CarPoolingWebApi.Models.Client;
using System.Collections.Generic;

namespace CarPoolingWebApi.Services.Interfaces
{
    public interface IBookingService
    {
        bool CreateBooking(Booking booking, string rideId);

        bool CancelRideRequest(string id);

        List<Booking> BookingsStatus(string id);

        bool BookingResponse(string id, BookingStatus status);

        string GetRequester(string id);

        List<Booking> GetUserBookings(string userId);

        List<Booking> GetBookings(string rideId);

        List<Booking> GetAllPendingBookings(string rideId);

        Booking GetBooking(string bookingId);
    }
}
