using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockLoyalty
    {
        public List<Loyalty> GetLoyalties()
        {
            return new List<Loyalty>{
                new Loyalty{
                    LoyaltyId=1,
                    LoyaltyPoints=40,
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ad")
                },
                new Loyalty{
                    LoyaltyId=2,
                    LoyaltyPoints=60,
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac")
                },
                new Loyalty{
                    LoyaltyId=3,
                    LoyaltyPoints=80,
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ab")
                }
            };
            
        }
    }
}