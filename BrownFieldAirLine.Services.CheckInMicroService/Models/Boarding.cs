using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table Boarding in Database
    ///<summary/>
    [Table("boarding")]
    public class Boarding
    {
        [Key]
        public int BoardingId { get; set; }
        public int BookingId { get; set; }
        public string? FlightNumber { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? SeatNumber { get; set; }
        public string? BoardingTime { get; set; }
        public string? DepartureTime { get; set; }
        public int PassengerId {get;set;}


        public Booking booking {get;set;}
        public Passenger passenger {get;set;}
    }
}