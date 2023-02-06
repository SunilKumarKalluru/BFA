using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository
{
    ///<summary>
    ///This interface holds functions related to checkin that are exposed in checkin controller
    ///<summary/>
    public interface ICheckInRepository
    {
        ///<summary>
        ///This function is used to get bookings for the PNR
        ///<summary/>
        Task<Booking> GetBookingByIdAsync(string PnrNo ,string Email);
        ///<summary>
        ///This function is used to get bookings for the PNR
        ///<summary/>
        Task<Booking> GetBookingByIdAsync(string pnrNo);
        ///<summary>
        ///This function is used to checkin a passenger with PNR
        ///<summary/>
         Task<bool> CheckIn(CheckInDto checkInDto);
         ///<summary>
        ///This function is used to get checkin details for the booking
        ///<summary/>
         Task<CheckIn> GetCheckIn(Booking booking);
         ///<summary>
        ///This function is used to validate checkin details for the PNR and email
        ///<summary/>
         Task<CheckInValidationDto> ValidateCheckinAsync(string PnrNo,string Email);
         ///<summary>
        ///This function is used to get booking details for the respective PNR
        ///<summary/>
         Task<CheckedInBookingsDto> CheckedInBookingDetailsAsync(string PnrNo,string Email);
        ///<summary>
        ///This function is used to validate checkin status for the PNR and email
        ///<summary/>
        Task<CheckInValidationDto> ValidateCheckinStatusAsync(Booking booking);

        
    }
}