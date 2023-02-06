using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to seats reserved passengers
    ///<summary/>
    public class SeatingExportDto
    {
        public List<string>? SeatsReserved { get; set; }
    }
}