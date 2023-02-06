using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository
{
    ///<summary>
    ///This interface holds functions related to loyalty that are exposed in loyalty controller
    ///<summary/>
    public interface ILoyaltyRepository
    {
        ///<summary>
        ///This function is used to add or update loyalty for passengers 
        ///<summary/>
        Task<Loyalty> GetLoyalty(Guid userid, int bookingId );
    }
}