using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Dtos
{
    ///<summary>
    ///This class holds properties related to reserved seat details of passengers
    ///<summary/>
    public class ReservedSeatsDto
    {
        public string? SeatNumber { get; set; }
    }
}