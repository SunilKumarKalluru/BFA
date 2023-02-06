using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Execution;
using BrownFieldAirLine.Services.CheckInMicroService.Controllers;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.ControllerTest
{
    public class CheckInControllerTest
    {
        private readonly Mock<ICheckInRepository> _checkInRepository;
        private readonly Mock<ILogger<CheckInController>> _logger;

        private readonly CheckInController _controller;
        public CheckInControllerTest()
        {
            _checkInRepository = new Mock<ICheckInRepository>();
            _logger = new Mock<ILogger<CheckInController>>();
            _controller = new CheckInController(_logger.Object,_checkInRepository.Object);
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CheckinController_Validate_CheckinDetails_Should_Return_200_ButValidation_True()
        {
            CheckInValidationDto expected = new CheckInValidationDto{
                checkinValidation = true
            };
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

            mockCheckinRepository.Setup(x=>x.ValidateCheckinAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new CheckInValidationDto{checkinValidation = true});

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.ValidateCheckin("KUERT","sunilkumar@gmail.com");

            var content = actionResult as OkObjectResult;
            
            var actualConfiguration = content.Value as CheckInValidationDto;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
            
        }

        [Test]
        public async Task CheckinController_Validate_CheckinDetails_Should_Return_200_But_Validation_False()
        {
            CheckInValidationDto expected = new CheckInValidationDto{
                checkinValidation = false
            };
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

            mockCheckinRepository.Setup(x=>x.ValidateCheckinAsync("KUER","sunilkumar@gmail.co")).ReturnsAsync(new CheckInValidationDto{checkinValidation = false});

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.ValidateCheckin("KUER","sunilkumar@gmail.co");

            var content = actionResult as OkObjectResult;
            
            var actualConfiguration = content.Value as CheckInValidationDto;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
            
        }

        [Test]
        public async Task CheckinController_CheckIn_Passenger_ShouldReturn_CheckIn_Status_True()
        {
            CheckinExportDto expected = new CheckinExportDto{
                CheckinStatus = true
            };
            Booking bookingDetails = new Booking{
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"
            };
            CheckInDto inputCheckIn = new CheckInDto{PnrNo="KUERT",Email="sunilkumar@gmail.com"};
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

            mockCheckinRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            mockCheckinRepository.Setup(x=>x.GetCheckIn(bookingDetails)).ReturnsAsync((CheckIn)null);

            mockCheckinRepository.Setup(x=>x.CheckIn(inputCheckIn)).ReturnsAsync(true);

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.CheckInPassenger(inputCheckIn);

            var content = actionResult as OkObjectResult;
            
            var actualConfiguration = content.Value as CheckinExportDto;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
            
        }
        [Test]
        public async Task CheckinController_CheckIn_Passenger_ShouldReturn_CheckIn_Status_False()
        {
            CheckinExportDto expected = new CheckinExportDto{
                CheckinStatus = false
            };
            Booking bookingDetails = new Booking{
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"
            };
            CheckInDto inputCheckIn = new CheckInDto{PnrNo="KUERT",Email="sunilkumar@gmail.com"};
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

            mockCheckinRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            mockCheckinRepository.Setup(x=>x.GetCheckIn(bookingDetails)).ReturnsAsync((CheckIn)null);

            mockCheckinRepository.Setup(x=>x.CheckIn(inputCheckIn)).ReturnsAsync(false);

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.CheckInPassenger(inputCheckIn);

            var content = actionResult as OkObjectResult;
            
            var actualConfiguration = content.Value as CheckinExportDto;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
            
        }
        [Test]
        public async Task CheckinController_CheckIn_Passenger_ShouldReturn_CheckIn_Status_400()
        {
            Booking bookingDetails = new Booking{
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"
            };
            CheckInDto inputCheckIn = new CheckInDto{PnrNo="KUERT",Email="sunilkumar@gmail.com"};
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

            mockCheckinRepository.Setup(x=>x.GetBookingByIdAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            mockCheckinRepository.Setup( x=>x.GetCheckIn(It.IsAny<Booking>())).ReturnsAsync(new CheckIn{
                    CheckInId=1,
                    BookingId=42578,
                    PnrNo="KUERT",
                    Email="sunilkumar@gmail.com",
                    LastName="reddy",
                    CheckInStatus=true,
                    CheckInTime=DateTime.Now
                });


            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.CheckInPassenger(inputCheckIn);

            var content = actionResult as ObjectResult;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            
        }
        [Test]
        public async Task CheckinController_CheckIn_Passenger_ShouldReturn_Booking_Details_Of_CheckedInPassenger()
        {
            var expected = new CheckedInBookingsDto{
            FirstName="sunil",
            LastName="reddy",
            FlightNumber="BF99392",
            NumberOfPassengers=3,
            SourceCity="DEL",
            DestinationCity="HYD",
            PnrNo="KUERT",
            Email="sunilkumar@gmail.com",
           };
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

           mockCheckinRepository.Setup(x=>x.CheckedInBookingDetailsAsync("KUERT","sunilkumar@gmail.com")).ReturnsAsync(new CheckedInBookingsDto{
            FirstName="sunil",
            LastName="reddy",
            FlightNumber="BF99392",
            NumberOfPassengers=3,
            SourceCity="DEL",
            DestinationCity="HYD",
            PnrNo="KUERT",
            Email="sunilkumar@gmail.com",
           });

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.GetCheckedInDetails("KUERT","sunilkumar@gmail.com");

            var content = actionResult as OkObjectResult;

            var actualConfiguration = content.Value as CheckedInBookingsDto;


            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
            
        }

        [Test]
        public async Task CheckinController_CheckIn_Passenger_Should_Return_Something_Went_Wrong_As_Booking_Details_Of_Checked_In_Passenger_Is_Wrong()
        {
            ResponseMessage expected = new ResponseMessage{
                Message = "something went wrong"
            };
            var mockCheckinRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<CheckInController>>();

           mockCheckinRepository.Setup(x=>x.CheckedInBookingDetailsAsync("KUER","sunilkumar@gmail.com")).ReturnsAsync((CheckedInBookingsDto)null);

            var controller = new CheckInController(logger.Object,mockCheckinRepository.Object);

            var actionResult = await controller.GetCheckedInDetails("KUERT","sunilkumar@gmail.com");

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            actualConfiguration.Message.Should().BeEquivalentTo(expected.Message);
            
        }
       
    }
}