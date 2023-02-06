using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository
{
    ///<summary>
    ///This interface holds functions related to boarding that are exposed in boarding controller
    ///<summary/>
    public interface IBoardingRepository
    {
        ///<summary>
        ///This function is used to get boarding passes for the booking
        ///<summary/>
         Task<List<BoardingPassDto>> GetBoardingPass(Booking booking);
    }
}