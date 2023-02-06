using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockBaggage
    {
        public List<Baggage> GetBaggages()
        {
             List<Baggage> baggages = new List<Baggage>{
                 new Baggage {
                    BaggageId=1,
                    BaggageWeight=5,
                    BagTag="",
                    PassengerId=1,
                    BookingId=42578,
                },
                new Baggage {
                    BaggageId=4,
                    BaggageWeight=5,
                    BagTag="",
                    PassengerId=4,
                    BookingId=42578,
                },
                new Baggage {
                    BaggageId=2,
                    BaggageWeight=10,
                    BagTag="FGH5671",
                    PassengerId=3,
                    BookingId=67834,
                },
                new Baggage {
                    BaggageId=3,
                    BaggageWeight=15,
                    BagTag="DFG6376",
                    PassengerId=2,
                    BookingId=53742,
                }
            };

            return baggages;
        }
        public List<BagTagExport> GetBagTagExports()
        {
            return new List<BagTagExport>{
                new BagTagExport{
                    FirstName="sunil",
                    LastName="reddy",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    DepartureDate="05-01-2023",
                    ArivalDate="06-01-2023",
                    BagTag="BVEI9726",
                    Weight="3KG"

                },
                new BagTagExport{
                    FirstName="mallesh",
                    LastName="yadav",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    DepartureDate="05-01-2023",
                    ArivalDate="06-01-2023",
                    BagTag="KKJP3272",
                    Weight="5KG"

                },
                new BagTagExport{
                    FirstName="razak",
                    LastName="enukondu",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    DepartureDate="05-01-2023",
                    ArivalDate="06-01-2023",
                    BagTag="WENY1199",
                    Weight="15KG"

                }
            };
            
        }
    }
}