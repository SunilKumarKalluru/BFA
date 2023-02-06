using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to CheckedInBooking details
    ///<summary/>
    public class CheckedInBookingsDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FlightNumber {get;set;}

        public int NumberOfPassengers {get;set;}

        public string? SourceCity {get;set;}

        public string? DestinationCity {get;set;}

        public string? PnrNo {get;set;}

        public string? Email {get;set;}

        public string? TravelDate{get;set;}

        public string? TravelClass {get;set;}
    }
}