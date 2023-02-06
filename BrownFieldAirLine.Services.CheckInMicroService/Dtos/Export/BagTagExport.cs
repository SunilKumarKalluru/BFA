using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to BagTagExport
    ///<summary/>
    public class BagTagExport
    {
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? FlightNumber {get;set;}
        public string? DepartureDate {get;set;}
        public string? ArivalDate {get;set;}
        public string? BagTag {get;set;}
        public string? Weight {get;set;}

    }
}