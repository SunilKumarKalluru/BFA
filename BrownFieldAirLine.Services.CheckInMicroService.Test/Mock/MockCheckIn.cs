using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockCheckIn
    {
        public List<CheckIn> GetCheckIns()
        {
            return new List<CheckIn>{
                new CheckIn{
                    CheckInId=1,
                    BookingId=42578,
                    PnrNo="KUERT",
                    Email="sunilkumar@gmail.com",
                    LastName="reddy",
                    CheckInStatus=true,
                    CheckInTime=DateTime.Now
                },
                new CheckIn{
                    CheckInId=2,
                    BookingId=53742,
                    PnrNo="JCPKPJF",
                    Email="sunilkumar@gmail.com",
                    LastName="reddy",
                    CheckInStatus=true,
                    CheckInTime=DateTime.Now
                }
            };
        }
        public CheckIn GetCheckInByBooking(int BookingId)
        {
            List<CheckIn> checkIns =  new List<CheckIn>{
                new CheckIn{
                    CheckInId=1,
                    BookingId=42578,
                    PnrNo="KUERT",
                    Email="sunilkumar@gmail.com",
                    LastName="reddy",
                    CheckInStatus=true,
                    CheckInTime=DateTime.Now
                },
                new CheckIn{
                    CheckInId=2,
                    BookingId=53742,
                    PnrNo="JCPKPJF",
                    Email="sunilkumar@gmail.com",
                    LastName="reddy",
                    CheckInStatus=true,
                    CheckInTime=DateTime.Now
                }
            };

            return checkIns.Where(x=>x.BookingId == BookingId).FirstOrDefault();
        }
    }
}