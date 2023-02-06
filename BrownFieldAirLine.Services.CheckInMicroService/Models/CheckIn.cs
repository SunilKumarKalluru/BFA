using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table CheckIn in Database
    ///<summary/>
    [Table("check_in")]
    public class CheckIn
    {
        [Key]
        public int CheckInId { get; set; }
        public int BookingId {get;set;}
        public string PnrNo { get; set; }
        public string Email {get;set;}
        public string LastName { get; set; }
        public bool CheckInStatus {get;set;}
        public DateTime CheckInTime { get; set; }

        public Booking booking {get;set;}
    }
}