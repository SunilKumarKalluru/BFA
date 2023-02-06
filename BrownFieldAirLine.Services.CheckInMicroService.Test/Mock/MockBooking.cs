using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockBooking
    {
       
        public  List<Booking> GetBookings()
        {
            List<Booking> bookings = new List<Booking>{
            new Booking{
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"
                },
                new Booking{
                    BookingId=53742,
                    PnrNo="JCPKPJF",
                    SourceCity="MAA",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="05-01-2023 07:49:58",
                    ClassName="Economy"
                },
                new Booking{
                    BookingId=67834,
                    PnrNo="SCLST",
                    SourceCity="MAA",
                    DestinationCity="DEL",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="05-01-2023 07:49:58",
                    ClassName="Economy"
                }
        };

           return bookings;
        }

        public  Booking GetBookingsById(string PNR)
        {
            List<Booking> bookings = new List<Booking>{
            new Booking{
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="05-01-2023 07:49:58",
                    ClassName="Economy"
                },
                new Booking{
                    BookingId=53742,
                    PnrNo="JCPKPJF",
                    SourceCity="MAA",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="05-01-2023 07:49:58",
                    ClassName="Economy"
                },
                new Booking{
                    BookingId=67834,
                    PnrNo="SCLST",
                    SourceCity="MAA",
                    DestinationCity="DEL",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="05-01-2023 07:49:58",
                    ClassName="Economy"
                }
        };

           return bookings.Where(x=>x.PnrNo.ToLower() == PNR.ToLower()).FirstOrDefault();
        }
    }
}