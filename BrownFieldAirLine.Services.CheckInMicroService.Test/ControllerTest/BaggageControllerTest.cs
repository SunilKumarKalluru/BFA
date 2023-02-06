using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Controllers;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Test.Mock;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.ControllerTest
{
    public class BaggageControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task BaggageController_BookBaggage_Should_Return_200_For_User_Details()
        {
            BaggageBookingDto baggageBookingDto = new BaggageBookingDto {
            Email="sunilkumar@gmail.com",
            BaggageWeight = new List<int> {
                5,10,15
            }};

            ResponseMessage expected = new ResponseMessage{
                Message = "BaggageBooked"
            };


            var baggageRepository = new Mock<IBaggageRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<BaggageController>>();
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
            baggageRepository.Setup(x=>x.GetPassengerDetails(42578)).ReturnsAsync(new List<Passenger>{
                new Passenger
                {
                    PassengerId=1,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=42578,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=2,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=42578,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=3,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=42578,
                    PhoneNo="7989296623"
                }
            });
            baggageRepository.Setup(x=>x.GetBaggageAsync(It.IsAny<int>(),baggageBookingDto)).ReturnsAsync(new List<Baggage>{});
            baggageRepository.Setup(x=>x.BaggageBooking(It.IsAny<Booking>(),It.IsAny<BaggageBookingDto>())).ReturnsAsync(true);

            var controller = new BaggageController(checkInRepository.Object,baggageRepository.Object,logger.Object);

            var actionResult = await controller.BaggageBooking("KUERT",baggageBookingDto);

            var content = actionResult as OkObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;


            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Message.Should().BeEquivalentTo(expected.Message);
        }

          [Test]
        public async Task BaggageController_BookBaggage_Should_Return_400_For_User_Details_Whose_Already_Booked_Baggage()
        {
            BaggageBookingDto baggageBookingDto = new BaggageBookingDto {
            Email="sunilkumar@gmail.com",
            BaggageWeight = new List<int> {
                5,10,15
            }};
            ResponseMessage expected = new ResponseMessage{
                Message = "Baggage already booked"
            };

            var baggageRepository = new Mock<IBaggageRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<BaggageController>>();
            checkInRepository.Setup(x=>x.GetBookingByIdAsync("KUERT",baggageBookingDto.Email)).ReturnsAsync(new Booking{ 
                    BookingId=42578,
                    PnrNo="KUERT",
                    SourceCity="DEL",
                    DestinationCity="HYD",
                    FlightNumber="BF99392",
                    NoOfPassengers=3,
                    BookingDate="04-01-2023 07:49:58",
                    TravelDate="24-01-2023 20:10:58",
                    ClassName="Economy"});
            baggageRepository.Setup(x=>x.GetPassengerDetails(42578)).ReturnsAsync(new List<Passenger>{
                new Passenger
                {
                    PassengerId=1,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=42578,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=2,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=42578,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=3,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=42578,
                    PhoneNo="7989296623"
                }
            });
            baggageRepository.Setup(x=>x.GetBaggageAsync(It.IsAny<int>(),baggageBookingDto)).ReturnsAsync(new List<Baggage>{
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
                        }
                        });
           
            var controller = new BaggageController(checkInRepository.Object,baggageRepository.Object,logger.Object);

            var actionResult = await controller.BaggageBooking("KUERT",baggageBookingDto);

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as ResponseMessage;


            Assert.IsNotNull(content);
            Assert.AreEqual(400,content.StatusCode);
            actualConfiguration.Message.Should().BeEquivalentTo(expected.Message);
        }

         [Test]
        public async Task BaggageController_Get_BaggageTags_Of_Passengers_Who_Booked_Baggage()
        {
            MockBaggage mockBaggage = new MockBaggage();
          
            List<BagTagExport> expected = mockBaggage.GetBagTagExports();

            var baggageRepository = new Mock<IBaggageRepository>();
            var checkInRepository = new Mock<ICheckInRepository>();
            var logger = new Mock<ILogger<BaggageController>>();
            
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
            baggageRepository.Setup(x=>x.GetBagTagAsync(It.IsAny<Booking>())).ReturnsAsync(mockBaggage.GetBagTagExports());
            
            var controller = new BaggageController(checkInRepository.Object,baggageRepository.Object,logger.Object);

            var actionResult = await controller.GetBaggageTags("KUERT");

            var content = actionResult as ObjectResult;

            var actualConfiguration = content.Value as List<BagTagExport>;


            Assert.IsNotNull(content);
            Assert.AreEqual(200,content.StatusCode);
            actualConfiguration.Should().BeEquivalentTo(expected);
        }
    }
}