using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Controllers;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.ControllerTest
{
    public class BoardingControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BoardingController_GetBoarding_Pass_For_CheckedIn_Passenger()
        {

            List <BoardingPassDto> expected = new List<BoardingPassDto>{
                new BoardingPassDto{
                    FirstName="sunil",
                    LastName="reddy",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="1A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                },
                new BoardingPassDto{
                    FirstName="mallesh",
                    LastName="yadav",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="2A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                },
                new BoardingPassDto{
                    FirstName="razak",
                    LastName="enukondu",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="3A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                }
            };
            var boardingRepository = new Mock<IBoardingRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<BoardingController>>();

            checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            boardingRepository.Setup(x=>x.GetBoardingPass(It.IsAny<Booking>())).ReturnsAsync(new List<BoardingPassDto>{
                new BoardingPassDto{
                    FirstName="sunil",
                    LastName="reddy",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="1A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                },
                new BoardingPassDto{
                    FirstName="mallesh",
                    LastName="yadav",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="2A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                },
                new BoardingPassDto{
                    FirstName="razak",
                    LastName="enukondu",
                    FromLocation="DEL",
                    ToLocation="HYD",
                    FlightNumber="BF99392",
                    SeatNumber="3A",
                    BoardingTime="09:53",
                    DepartureTime="10:38",
                    DepartureDate="19-01-2023",
                    ArivalDate="20-01-2023"
                }  
            });

            var controller = new BoardingController(logger.Object,checkInRepository.Object,boardingRepository.Object);


            var actionResult =await controller.GetBoardingPass("KUERT");

            var content = actionResult as OkObjectResult;

            var absoluteConfiguration = content.Value as List<BoardingPassDto>;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            absoluteConfiguration.Should().BeEquivalentTo(expected);
         
        }


        [Test]
        public async Task BoardingController_GetBoarding_Pass_For_CheckedIn_Passenger_Should_Return_400_Due_To_Internal_Bookings_Issue_Like_CheckIn_NotDone()
        {    ResponseMessage expected = new ResponseMessage{
                Message = "Something went wrong"
            };
            var boardingRepository = new Mock<IBoardingRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<BoardingController>>();

            checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            boardingRepository.Setup(x=>x.GetBoardingPass(It.IsAny<Booking>())).ReturnsAsync((List<BoardingPassDto>)null);

            var controller = new BoardingController(logger.Object,checkInRepository.Object,boardingRepository.Object);


            var actionResult =await controller.GetBoardingPass("KUERT");

            var content = actionResult as ObjectResult;

            var absoluteConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            absoluteConfiguration.Message.Should().BeEquivalentTo(expected.Message);
         
        }
    }
}