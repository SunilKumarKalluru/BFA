using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;

namespace BrownFieldAirLine.Services.CheckInMicroService.Mapping
{
    ///<summary>
    ///This mapper class maps properties of baggage import and baggage details for importing into database
    ///<summary/>
    public class BaggageMapper : Profile
    {
        public BaggageMapper()
        {
            CreateMap<BaggageWeightClassExport,BaggageWeightClass>().ReverseMap()
                .ForMember(
                    dest => dest.WeightClass,
                    opt => opt.MapFrom(src => $"{src.BaggageWeight}")
                );
        }
    }
}