using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Models
{
    ///<summary>
    ///This is model entity for table BaggageWeightClass in Database
    ///<summary/>
    [Table("baggage_weight_class")]
    public class BaggageWeightClass
    {
        [Key]
        public int BaggageId { get; set; }

        public int? BaggageWeight { get; set; }
    }
}