using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Controllers;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.ControllerTest
{
    public class SeatingControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SeatingController_Seating_Booking_For_User_Should_Return_200 ()
        {
            SeatBookingDto seatBookingDto = new SeatBookingDto{
                Email="sunilkumar@gmail.com",
                SeatNumber=new List<string>{
                    "1A","2A","3A"
                }
            };
            SeatingExportDto seatingExportDto = new SeatingExportDto{
                SeatsReserved = new List<string>{
                    "1A","2A","3A"
                }
            };

            var seatingRepository = new Mock<ISeatingRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<SeatingController>>();
             checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            seatingRepository.Setup(x=>x.GetBookedSeatAsync(42578)).ReturnsAsync(new List<Seating>{});
            seatingRepository.Setup(x=>x.CheckSeatsRequested(It.IsAny<Booking>(),It.IsAny<SeatBookingDto>())).ReturnsAsync(new List<ReservedSeatsDto>{});
            seatingRepository.Setup(x=>x.SeatBooking(It.IsAny<Booking>(),It.IsAny<SeatBookingDto>())).ReturnsAsync(true);

            var controller = new SeatingController(logger.Object,seatingRepository.Object,checkInRepository.Object);

            var actionResult =await controller.SeatBooking("KUERT",seatBookingDto);

            var content = actionResult as OkObjectResult;

            var actualConfiguration = content.Value as SeatingExportDto;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(seatingExportDto);


            
        }
        [Test]
        public async Task SeatingController_Seating_Booking_For_User_Should_Return_400_Seats_Already_Booked_Under_Booking ()
        {
            ResponseMessage expected = new ResponseMessage{
                Message= "Seats already booked under this booking"
            };
            SeatBookingDto seatBookingDto = new SeatBookingDto{
                Email="sunilkumar@gmail.com",
                SeatNumber=new List<string>{
                    "1A","2A","3A"
                }
            };
           
            var seatingRepository = new Mock<ISeatingRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<SeatingController>>();
             checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            seatingRepository.Setup(x=>x.GetBookedSeatAsync(42578)).ReturnsAsync(new List<Seating>{
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
                }
            });

            var controller = new SeatingController(logger.Object,seatingRepository.Object,checkInRepository.Object);

            var actionResult =await controller.SeatBooking("KUERT",seatBookingDto);

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            Assert.AreEqual(actualConfiguration.Message,expected.Message);
            


            
        }
        [Test]
        public async Task SeatingController_Seating_Booking_For_User_Should_Return_400_Seats_Requested_Are_Already_Booked_By_Another_Users ()
        {
            ResponseMessage expected = new ResponseMessage{
                Message= "Seats Already Reserved : 1A,2A,3A"
            };
            
            SeatBookingDto seatBookingDto = new SeatBookingDto{
                Email="sunilkumar@gmail.com",
                SeatNumber=new List<string>{
                    "1A","2A","3A"
                }
            };
           
            var seatingRepository = new Mock<ISeatingRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<SeatingController>>();
             checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            seatingRepository.Setup(x=>x.GetBookedSeatAsync(42578)).ReturnsAsync(new List<Seating>{
            });
            seatingRepository.Setup(x=>x.CheckSeatsRequested(It.IsAny<Booking>(),It.IsAny<SeatBookingDto>())).ReturnsAsync(new List<ReservedSeatsDto>{
                new ReservedSeatsDto{
                    SeatNumber="1A"
                },
                new ReservedSeatsDto{
                    SeatNumber="2A"
                },
                new ReservedSeatsDto{
                    SeatNumber="3A"
                }
            });

            var controller = new SeatingController(logger.Object,seatingRepository.Object,checkInRepository.Object);

            var actionResult =await controller.SeatBooking("KUERT",seatBookingDto);

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            Assert.AreEqual(actualConfiguration.Message,expected.Message);
            
        }
    }
}