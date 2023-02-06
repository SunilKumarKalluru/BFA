using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockSeating
    {
        public List<Seating> GetSeatings()
        {
            return new List<Seating>{
                new Seating{
                    SeatId=1,
                    SeatNumber="1A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=1,
                },
                new Seating{
                    SeatId=2,
                    SeatNumber="2A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=2,
                },
                new Seating{
                    SeatId=3,
                    SeatNumber="3A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=3,
                },
                new Seating{
                    SeatId=4,
                    SeatNumber="1B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=4,
                },
                new Seating{
                    SeatId=5,
                    SeatNumber="2B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=5,
                },
                new Seating{
                    SeatId=6,
                    SeatNumber="3B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=6,
                }
            };
            
        }
        public List<Seating> GetSeatingsByBooking(int BookingId)
        {
            List<Seating> seatings = new List<Seating>{
                new Seating{
                    SeatId=1,
                    SeatNumber="1A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=1,
                },
                new Seating{
                    SeatId=2,
                    SeatNumber="2A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=2,
                },
                new Seating{
                    SeatId=3,
                    SeatNumber="3A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=3,
                },
                new Seating{
                    SeatId=4,
                    SeatNumber="1B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=4,
                },
                new Seating{
                    SeatId=5,
                    SeatNumber="2B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=5,
                },
                new Seating{
                    SeatId=6,
                    SeatNumber="3B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=6,
                }
            };
            return seatings.Where(x=>x.BookingId == BookingId).ToList();
            
        }
        public Seating GetSeatingsByPassenger(int PassengerId)
        {
            List<Seating> seatings = new List<Seating>{
                new Seating{
                    SeatId=1,
                    SeatNumber="1A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=1,
                },
                new Seating{
                    SeatId=2,
                    SeatNumber="2A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=2,
                },
                new Seating{
                    SeatId=3,
                    SeatNumber="3A",
                    FlightNumber="BF99392",
                    BookingId=42578,
                    PassengerId=3,
                },
                new Seating{
                    SeatId=4,
                    SeatNumber="1B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=4,
                },
                new Seating{
                    SeatId=5,
                    SeatNumber="2B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=5,
                },
                new Seating{
                    SeatId=6,
                    SeatNumber="3B",
                    FlightNumber="BF99392",
                    BookingId=53742,
                    PassengerId=6,
                }
            };
            return seatings.Where(x=>x.PassengerId == PassengerId).FirstOrDefault();
            
        }
    }
}