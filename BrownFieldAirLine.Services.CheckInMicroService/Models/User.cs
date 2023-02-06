using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table User in Database
    ///<summary/>
    public class User
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public Loyalty loyalty {get;set;}
    }
}