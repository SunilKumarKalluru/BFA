using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table Passenger in Database
    ///<summary/>
    [Table("passenger")]
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public int? BookingId { get; set; }

        public Booking booking {get;set;}
        public Seating seating {get;set;}
        public Baggage baggage {get;set;}
        public Boarding boarding {get;set;}

    }
}