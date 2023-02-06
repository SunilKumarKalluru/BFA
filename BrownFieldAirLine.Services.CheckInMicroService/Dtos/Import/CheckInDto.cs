using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to import of checkin details
    ///<summary/>
    public class CheckInDto
    {
        public string? PnrNo { get; set; }
        public string? Email {get;set;}
    }
}