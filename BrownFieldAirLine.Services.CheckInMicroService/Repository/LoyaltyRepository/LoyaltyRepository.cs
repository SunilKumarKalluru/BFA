using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository
{
    public class LoyaltyRepository : ILoyaltyRepository
    {
        ///<summary>
        ///This class holds all functions related to loyalty that are exposed in loyalty controller and helper functions of it
        ///<summary/>
        private readonly BrownFieldAirLineContext _context;

        private readonly IMapper _mapper;
        ///<summary>
        ///This constructor has parameters of context class and mapper for dependency injection
        ///<summary/>
        public LoyaltyRepository(BrownFieldAirLineContext context,IMapper mapper)
        {
            _context=context; 
            _mapper=mapper;  
        }
        //<summary>
        ///This function is used to add or update loyalty for passengers 
        ///<summary/>
        public async Task<Loyalty> GetLoyalty(Guid userid, int bookingId )
        {
            var checkin = await _context.checkIns.Where(x=>x.BookingId == bookingId).FirstOrDefaultAsync();
            if(checkin != null)
            {
                var loyalty = await _context.loyalties.Where(x=>x.UserId == userid).FirstOrDefaultAsync();
                if(loyalty != null)
                {
                    loyalty.LoyaltyPoints = loyalty.LoyaltyPoints+20;
                    _context.loyalties.Update(loyalty);

                    await _context.SaveChangesAsync();
                    return loyalty;
                }
                else
                {
                    Loyalty loyaltyDetails = new Loyalty();
                    loyaltyDetails.LoyaltyPoints = 20;
                    loyaltyDetails.UserId = userid;

                    await _context.loyalties.AddAsync(loyaltyDetails);

                    await _context.SaveChangesAsync();
                    return loyalty;
                }

            }
            return null;
        }
    }
}