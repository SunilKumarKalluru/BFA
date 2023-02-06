using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockBoarding
    {
        public List<Boarding> GetBoardings()
        {
            return new List<Boarding> {
                new Boarding{
                    BoardingId=1,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="1A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=1,
                },
                new Boarding{
                    BoardingId=2,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="2A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=2,
                },
                new Boarding{
                    BoardingId=3,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="3A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=3,
                },
                new Boarding{
                    BoardingId=4,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="1B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=4,
                },
                new Boarding{
                    BoardingId=5,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="2B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=5,
                },
                new Boarding{
                    BoardingId=6,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="3B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=6,
                }
            };

        }
        public List<Boarding> GetBoardingsByBookingID(int BookingId)
        {
            List<Boarding> boardings =  new List<Boarding> {
                new Boarding{
                    BoardingId=1,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="1A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=1,
                },
                new Boarding{
                    BoardingId=2,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="2A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=2,
                },
                new Boarding{
                    BoardingId=3,
                    BookingId=42578,
                    FlightNumber="BF99392",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    SeatNumber="3A",
                    BoardingTime="19-01-2023 08:13:35",
                    DepartureTime="19-01-2023 08:58:35",
                    PassengerId=3,
                },
                new Boarding{
                    BoardingId=4,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="1B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=4,
                },
                new Boarding{
                    BoardingId=5,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="2B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=5,
                },
                new Boarding{
                    BoardingId=6,
                    BookingId=53742,
                    FlightNumber="BF99392",
                    FromLocation="MAA",
                    ToLocation="HYD",
                    SeatNumber="3B",
                    BoardingTime="20-01-2023 09:21:34",
                    DepartureTime="20-01-2023 10:06:34",
                    PassengerId=6,
                }
            };
            return boardings.Where(x=>x.BookingId == BookingId).ToList();
        }
    }
}