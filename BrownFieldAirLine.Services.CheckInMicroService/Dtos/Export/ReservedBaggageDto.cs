using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to reserved baggage details of passengers
    ///<summary/>
    public class ReservedBaggageDto
    {
        public string? LastName {get;set;}

        public int BaggageWeight {get;set;}
    }
}