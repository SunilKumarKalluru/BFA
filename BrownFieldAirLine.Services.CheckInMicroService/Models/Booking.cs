using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table Booking in Database
    ///<summary/>
    [Table("booking")]
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string? PnrNo {get;set;}
        public string? SourceCity { get; set; }
        public string? DestinationCity {get;set;}
        public string? FlightNumber {get;set;}
        public int NoOfPassengers {get;set;}
        public string? BookingDate {get;set;}
        public string? TravelDate {get;set;}
        public string? ClassName {get;set;}

        // Navigation Properties
        public List<Baggage> baggage {get;set;}
        public List<Boarding> boarding {get;set;}
        public List<Seating> seating {get;set;}
        public CheckIn checkIn {get;set;}
        public List<Passenger> passengers {get;set;}
    }
}