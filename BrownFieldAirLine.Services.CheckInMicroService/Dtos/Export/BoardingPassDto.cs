using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to BoardingPass
    ///<summary/>
    public class BoardingPassDto
    {
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? FlightNumber {get;set;}
        public string? SeatNumber { get; set; }
        public string? BoardingTime { get; set; }
        public string? DepartureTime {get;set;}
        public string? DepartureDate {get;set;}
        public string? ArivalDate {get;set;}

    }
}