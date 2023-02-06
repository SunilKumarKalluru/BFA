using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to booking of baggage details for passengers
    ///<summary/>
    public class BaggageBookingDto
    {
        public string? Email {get;set;}
        public List<int>? BaggageWeight {get;set;}
    }
}