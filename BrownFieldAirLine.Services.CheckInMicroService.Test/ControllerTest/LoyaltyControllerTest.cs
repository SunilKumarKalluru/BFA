using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Controllers;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.ControllerTest
{
    public class LoyaltyControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoyaltyController_GetUpdatedLoyalty_Points_On_Hitting_EndPoint_For_Existing_Users()
        {
            ResponseMessage expected = new ResponseMessage{
                Message = "Current Loyalty Points :50"
            };
            var loyaltyRepository = new Mock<ILoyaltyRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<LoyaltyController>>();

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
            loyaltyRepository.Setup(x=>x.GetLoyalty(Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac"),42578)).ReturnsAsync(new Loyalty{
                LoyaltyId =1,
                LoyaltyPoints=50,
                UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac")
            });

            var controller = new LoyaltyController(logger.Object,loyaltyRepository.Object,checkInRepository.Object);

            var actionResult = await controller.GetLoylaty(Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac"),"KUERT");

            var content = actionResult as OkObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            Assert.AreEqual(actualConfiguration.Message,expected.Message);

        }
        [Test]
        public async Task LoyaltyController_GetUpdatedLoyalty_Shold_Return_Booking_Not_Found()
        {
            ResponseMessage expected = new ResponseMessage{
                Message = "something went wrong"
            };
            var loyaltyRepository = new Mock<ILoyaltyRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<LoyaltyController>>();

            checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUER")).ReturnsAsync((Booking)null);

            var controller = new LoyaltyController(logger.Object,loyaltyRepository.Object,checkInRepository.Object);

            var actionResult = await controller.GetLoylaty(Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac"),"KUERT");

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;

            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            actualConfiguration.Message.Should().BeEquivalentTo(expected.Message);

        }
    }
}