using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to Check in status
    ///<summary/>

    public class CheckinExportDto
    {
        public bool CheckinStatus { get; set; }
    }
}