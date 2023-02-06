using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository
{
    ///<summary>
    ///This interface holds functions related to baggage that are exposed in baggage controller
    ///<summary/>
    public interface IBaggageRepository
    {
        ///<summary>
        ///This function is used to book baggage for passengers and return status of it in boolean
        ///<summary/>
        Task<bool> BaggageBooking(Booking booking,BaggageBookingDto baggageBookingDto);

        ///<summary>
        ///This function is used to get no of passengers under the booking and return integer
        ///<summary/>
        Task<int> GetNoOfPassengersForBaggageAsync(Booking booking);

        ///<summary>
        ///This function is used to get passengers details under the booking and return list of passenger models
        ///<summary/>
         Task<List<Passenger>> GetPassengerDetails(int BookingId);

         ///<summary>
        ///This function is used to get reserved baggage details under the booking and return list of reservedbaggage dtos
        ///<summary/>
        Task<List<ReservedBaggageDto>> GetReservedBaggageAsync(Booking booking);

        ///<summary>
        ///This function is used to get baggage details under the booking and return list of baggage models
        ///<summary/>
        Task<List<Baggage>> GetBaggageAsync(int bookingId,BaggageBookingDto baggageBookingDtos);
        
        ///<summary>
        ///This function is used to get bagtag details under the booking and return list of bagtagexport dtos
        ///<summary/>
        Task<List<BagTagExport>> GetBagTagAsync(Booking booking);

        ///<summary>
        ///This function is used to get baggage classes
        ///<summary/>
        Task<List<BaggageWeightClassExport>> GetBaggageWeightClassAsync();
    }
}