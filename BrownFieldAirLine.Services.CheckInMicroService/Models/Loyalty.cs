using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table Loyalty in Database
    ///<summary/>
    [Table("loyalty")]
    public class Loyalty
    {
        [Key]
        public long LoyaltyId { get; set; }
        public int LoyaltyPoints { get; set; }
        public Guid UserId { get; set; }

        public User user {get;set;}
    }
}