using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to importing of seating details
    ///<summary/>
    public class SeatBookingDto
    {
        public string? Email { get; set; }
        public List<string>? SeatNumber {get;set;}
    }
}