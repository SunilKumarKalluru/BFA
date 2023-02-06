using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockPassenger
    {
        public List<Passenger> GetPassengers()
        {
            return new List<Passenger>{
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
                },
                new Passenger
                {
                    PassengerId=4,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=53742,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=5,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=53742,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=6,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=53742,
                    PhoneNo="7989296623"
                },
                new Passenger
                {
                    PassengerId=7,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=67834,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=8,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=67834,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=9,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=67834,
                    PhoneNo="7989296623"
                }
            };
            
        }
        public Passenger GetPassengerById(int PassengerId)
        {
            List<Passenger> passengers =  new List<Passenger>{
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
                },
                new Passenger
                {
                    PassengerId=4,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=53742,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=5,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=53742,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=6,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=53742,
                    PhoneNo="7989296623"
                },
                new Passenger
                {
                    PassengerId=7,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=67834,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=8,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=67834,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=9,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=67834,
                    PhoneNo="7989296623"
                }
            };

            return passengers.Where(x=>x.PassengerId == PassengerId).FirstOrDefault();

            
        }
        public List<Passenger> GetPassengerByBooking(int BookingId)
        {
            List<Passenger> passengers =  new List<Passenger>{
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
                },
                new Passenger
                {
                    PassengerId=4,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=53742,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=5,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=53742,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=6,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=53742,
                    PhoneNo="7989296623"
                },
                new Passenger
                {
                    PassengerId=7,
                    FirstName="sunil",
                    MiddleName="kumar",
                    LastName="reddy",
                    Email="sunilkumar@gmail.com",
                    BookingId=67834,
                    PhoneNo="9092992212"
                },
                new Passenger
                {
                    PassengerId=8,
                    FirstName="mallesh",
                    MiddleName="siva",
                    LastName="yadav",
                    Email="mallesh@gmail.com",
                    BookingId=67834,
                    PhoneNo="9100938822"
                },
                new Passenger
                {
                    PassengerId=9,
                    FirstName="razak",
                    MiddleName="",
                    LastName="enukondu",
                    Email="razak@gmail.com",
                    BookingId=67834,
                    PhoneNo="7989296623"
                }
            };
            return passengers.Where(x=>x.BookingId == BookingId).ToList();

        }
    }
}