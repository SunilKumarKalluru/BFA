using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Test.Mock;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.RepositoryTest
{
    public class RepositoryServiceTest
    {
        DbContextOptions<BrownFieldAirLineContext> options = new DbContextOptionsBuilder<BrownFieldAirLineContext>()
            .UseInMemoryDatabase(databaseName: "CheckInMicroService")
            .Options;
        public RepositoryServiceTest()
        {
            MockBoarding mockBoarding = new MockBoarding();
            MockBooking mockBooking = new MockBooking();
            MockBaggage mockBaggage = new MockBaggage();
            MockPassenger mockPassenger = new MockPassenger();
            MockSeating mockSeating = new MockSeating();
            MockUser mockUser = new MockUser();
            MockCheckIn mockCheckIn = new MockCheckIn();
            MockLoyalty mockLoyalty = new MockLoyalty();
            using (var context = new BrownFieldAirLineContext(options))
            {
                var bookings = mockBooking.GetBookings();
                var baggages = mockBaggage.GetBaggages();
                var passengers = mockPassenger.GetPassengers();
                var boardings = mockBoarding.GetBoardings();
                var seatings = mockSeating.GetSeatings();
                var users = mockUser.GetUsers();
                var loyalties = mockLoyalty.GetLoyalties();
                var checkins = mockCheckIn.GetCheckIns();

                context.bookings.AddRange(bookings);
                context.passengers.AddRange(passengers);
                context.seatings.AddRange(seatings);
                context.boardings.AddRange(boardings);
                context.users.AddRange(users);
                context.checkIns.AddRange(checkins);
                context.loyalties.AddRange(loyalties);
                 context.baggages.AddRange(baggages);
                context.SaveChanges();
            }
            
        }
        [SetUp]
        public async Task Setup()
        {
            
        }

        [Test]
        public async Task SeatBooking_Service_Should_Return_True()
        {
                SeatBookingDto seatBookingDto = new SeatBookingDto{
                    Email = "sunilkumar@gmail.com",
                    SeatNumber = new List<string>{
                        "1A","2A","3A"
                    }
                };
                using(var context = new BrownFieldAirLineContext(options))
                {
                    MockBooking mockBooking = new MockBooking();
                    var _mapper = new Mock<IMapper>();
                    SeatingRepository seatingRepository = new SeatingRepository(context,_mapper.Object);

                    var result = await seatingRepository.SeatBooking(mockBooking.GetBookingsById("KUERT"),seatBookingDto);

                    bool expectedresult = true;
                    Assert.AreEqual(expectedresult,result);

                }
        }
        [Test]
        public async Task UpdateLoyalty_Points_On_Successful_Journey()
        {
            Loyalty expectedresult = new Loyalty{
                LoyaltyId =2,
                LoyaltyPoints=80,
                UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac")
            };
           
            using (var context = new BrownFieldAirLineContext(options))
            {
               var _mapper = new Mock<IMapper>();
                LoyaltyRepository loyaltyRepository = new LoyaltyRepository(context, _mapper.Object);

                var result = await loyaltyRepository.GetLoyalty(Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac"),42578);

                bool equalornot = JsonCompare(result,expectedresult);
                    Assert.IsTrue(equalornot);
            }
        }
        [Test]
        public async Task CheckIn_Passenger_Before_Departure()
        {
            bool expectedresult = false;
            CheckInDto checkInDto = new CheckInDto{
                PnrNo="KUERT",
                Email="sunilkumar@gmail.com"
            };
            using (var context = new BrownFieldAirLineContext(options))
            {
               var _mapper = new Mock<IMapper>();
                CheckInRepository checkInRepository = new CheckInRepository(context, _mapper.Object);

                var result = await checkInRepository.CheckIn(checkInDto);

                Assert.AreEqual(expectedresult,result);
            }
        }
        [Test]
        public async Task GetBoardingPass_Expected_To_Have_Boarding_Passes()
        {
            MockBooking mockBooking = new MockBooking();
            List<BoardingPassDto> expectedresult = new List<BoardingPassDto>{
                new BoardingPassDto{
                        ArivalDate ="20-01-2023",
                        BoardingTime ="08:13",
                        DepartureDate ="19-01-2023",
                        DepartureTime ="08:58",
                        FirstName ="sunil",
                        FlightNumber ="BF99392",
                        FromLocation ="DEL",
                        LastName ="reddy",
                        SeatNumber ="1A",
                        ToLocation ="HYD"
                },
                new BoardingPassDto{
                        ArivalDate ="20-01-2023",
                        BoardingTime ="08:13",
                        DepartureDate ="19-01-2023",
                        DepartureTime ="08:58",
                        FirstName ="mallesh",
                        FlightNumber ="BF99392",
                        FromLocation ="DEL",
                        LastName ="yadav",
                        SeatNumber ="2A",
                        ToLocation ="HYD"
                },
                new BoardingPassDto{
                        ArivalDate ="20-01-2023",
                        BoardingTime ="08:13",
                        DepartureDate ="19-01-2023",
                        DepartureTime ="08:58",
                        FirstName ="razak",
                        FlightNumber ="BF99392",
                        FromLocation ="DEL",
                        LastName ="enukondu",
                        SeatNumber ="3A",
                        ToLocation ="HYD"
                }

            };
            using (var context = new BrownFieldAirLineContext(options))
            {
                var _mapper = new Mock<IMapper>();
                BoardingRepository boardingRepository = new BoardingRepository(context, _mapper.Object);

                var result = await boardingRepository.GetBoardingPass(mockBooking.GetBookingsById("KUERT"));

                bool equalornot = JsonCompare(result,expectedresult);

                
                
                Assert.IsTrue(!equalornot);

            }
        }
        [Test]
        public async Task BaggageBooking_Service_Should_Return_True()
        {
             List<Baggage> resultexpected = new List<Baggage> {
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
                }
            };
        BaggageBookingDto baggageBookingDto = new BaggageBookingDto {
            Email="sunilkumar@gmail.com",
            BaggageWeight = new List<int> {
                5,10,15
            }};
            MockBooking mockBooking = new MockBooking();
            
                using(var context = new BrownFieldAirLineContext(options))
                {
                    var _mapper = new Mock<IMapper>();
                    BaggageRepository baggageRepository = new BaggageRepository(context,_mapper.Object);

                    var result = await baggageRepository.BaggageBooking(mockBooking.GetBookingsById("KUERT"),baggageBookingDto);

                    bool expectedresult = true;
                    Assert.AreEqual(expectedresult,result);

                }

        }
        public static bool CheckBools(bool expectedresult,bool actual)
        {
            if(expectedresult == actual)
            {
                return true;
            }
            else{
                return false;
            }
        }
        public static bool JsonCompare(object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }
    }
}