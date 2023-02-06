using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository
{
    ///<summary>
    ///This interface holds functions related to seating that are exposed in seating controller
    ///<summary/>
    public interface ISeatingRepository
    {
        ///<summary>
        ///This function is used to check whether requested seats for passengers
        ///<summary/>
        Task<List<ReservedSeatsDto>> CheckSeatsRequested(Booking booking,SeatBookingDto seatBookingDto);
        ///<summary>
        ///This function is used to fetch seats booked under PNR  
        ///<summary/>
        Task<List<Seating>> GetBookedSeatAsync(int bookingId);
        ///<summary>
        ///This function is used to fetch seats booked under PNR  
        ///<summary/>
        Task<List<ReservedSeatsDto>> GetBookedSeatByUserAsync(int bookingId);
        ///<summary>
        ///This function is used to book seats for the passengers
        ///<summary/>
        Task<bool> SeatBooking(Booking booking,SeatBookingDto seatBookingDto);
        ///<summary>
        ///This function is used to no of passengers under the booking
        ///<summary/>
        Task<int> GetNoOfPassengersForSeatingAsync(Booking booking);
        ///<summary>
        ///This function is used to fetch seats booked under flight related to PNR  
        ///<summary/>
        Task<List<ReservedSeatsDto>> GetReservedSeatsAsync(Booking booking);
    }
}