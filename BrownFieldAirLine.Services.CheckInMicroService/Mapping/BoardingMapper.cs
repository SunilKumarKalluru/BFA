using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Mapping
{
    ///<summary>
    ///This mapper class maps properties of boarding and boarding pass dto
    ///<summary/>
    public class BoardingMapper : Profile
    {
        public BoardingMapper()
        {
            CreateMap<Boarding,BoardingPassDto>().ReverseMap();
        }
    }
}