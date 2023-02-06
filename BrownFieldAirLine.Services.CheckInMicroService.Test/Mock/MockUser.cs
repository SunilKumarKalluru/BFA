using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Test.Mock
{
    public class MockUser
    {
        public List<User> GetUsers()
        {
            return new List<User>{
                new User{
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ab"),
                    UserName="Sunilkumar",
                    Password="1234567",
                },
                new User{
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ac"),
                    UserName="SunilK",
                    Password="1234567",
                },
                new User{
                    UserId=Guid.Parse("0aae66fd-8577-46d8-8593-93746d51e4ad"),
                    UserName="SunilKalluru",
                    Password="1234567",
                }
            };
            
        }
    }
}