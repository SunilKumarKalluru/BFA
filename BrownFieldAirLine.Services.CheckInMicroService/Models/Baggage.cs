using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table baggage in Database
    ///<summary/>
    [Table("baggage")]
    public class Baggage
    {
        [Key]
        public int BaggageId { get; set; }
        public int BaggageWeight { get; set; }

        [Required]
        public int PassengerId {get;set;}
        public string? BagTag { get; set; }
        public int BookingId { get; set; }
        public Booking booking {get;set;}
        public Passenger passenger {get;set;}

    }
}