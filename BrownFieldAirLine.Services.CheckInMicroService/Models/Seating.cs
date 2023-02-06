using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table Seating in Database
    ///<summary/>
    [Table("seating")]
    public class Seating
    {
        [Key]
        public int SeatId { get; set; }
        public string? SeatNumber { get; set; }
        public string? FlightNumber {get;set;}
        public int? BookingId { get; set; }
        public int? PassengerId {get;set;}

        public Booking booking {get;set;}
        public Passenger passenger {get;set;}

    }
}