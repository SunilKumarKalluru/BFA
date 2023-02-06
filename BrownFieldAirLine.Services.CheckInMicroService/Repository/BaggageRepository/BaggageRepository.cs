using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository
{
    ///<summary>
    ///This class holds all functions related to baggage that are exposed in baggage controller and helper functions of it
    ///<summary/>
    public class BaggageRepository : IBaggageRepository
    {
        private readonly BrownFieldAirLineContext _context;

        private readonly IMapper _mapper;
        ///<summary>
        ///This constructor has parameters of context class and mapper for dependency injection
        ///<summary/>
        public BaggageRepository(BrownFieldAirLineContext context,IMapper mapper)
        {
            _context=context; 
            _mapper=mapper;  
        }
        ///<summary>
        ///This function is used to generate an random id used for baggage returns an integer
        ///<summary/>
        private int BaggageIDGenerator()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 5;
            string finaldigit;
            int getindex;
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            var baggages = _context.baggages.ToList();
            foreach(var item in baggages)
            {
                if(item.BaggageId.ToString() == ID)
                {
                    BaggageIDGenerator();
                }
            }
            return Convert.ToInt32(ID);
          
        }
        ///<summary>
        ///This function is used to generate an random tag used for baggage returns an string
        ///<summary/>
        private string PrintBagTag()
        {
            string alphabets ="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "123456789";
            int length =4;
            var chars = new char[length];
            var numArr = new char[length];
            var rd = new Random();
            for (var i = 0; i < length; i++)
            {
                chars[i] = alphabets[rd.Next(0, alphabets.Length)];
            }
            for (var i = 0; i < length; i++)
            {
                numArr[i] = numbers[rd.Next(0, numbers.Length)];
            }

            return new String(chars)+new String(numArr);
        }
        ///<summary>
        ///This function is used to get baggage details under the booking and return list of baggage models
        ///<summary/>
        public async Task<List<Baggage>> GetBaggageAsync(int bookingId,BaggageBookingDto baggageBookingDtos)
        {
            List<Baggage> baggages = new List<Baggage>();

            var baggageList = await _context.baggages.Where(x=>x.BookingId ==bookingId).ToListAsync();
            if(baggageList.Count > 0)
            {
                baggages = baggageList;
                return baggages;
            }
            return baggages;
        }
        ///<summary>
        ///This function is used to get passengers details under the booking and return list of passenger models
        ///<summary/>
        public async Task<List<Passenger>> GetPassengerDetails(int BookingId)
        {
            List<Passenger> passengers = new List<Passenger>();
            var passenger = await _context.passengers.Where(x => x.BookingId == BookingId).ToListAsync();

            if(passenger.Count > 0)
            {
                passengers = passenger;
                return passengers;
            }
            return passengers;
        }
        ///<summary>
        ///This function is used to get passengers details by passenger id and returns passenger model
        ///<summary/>
        private Passenger GetPassengerById(int passegerId)
        {
            var passeger = _context.passengers.Where(x => x.PassengerId == passegerId).FirstOrDefault();

            
            if(passeger != null)
            {
                return passeger;
            }
            return null;
        }
        ///<summary>
        ///This function is used to get reserved baggage details under the booking and return list of reservedbaggage dtos
        ///<summary/>
        public async Task<List<ReservedBaggageDto>> GetReservedBaggageAsync(Booking booking)
        {
            List<ReservedBaggageDto> reservedBaggageDtos = new List<ReservedBaggageDto>();
            var baggageDetails = await _context.baggages.Where(x=>x.BookingId == booking.BookingId).ToListAsync();

            foreach(var baggageDetail in baggageDetails)
            {
                ReservedBaggageDto reservedBaggageDto = new ReservedBaggageDto();
                Passenger passenger = GetPassengerById(baggageDetail.PassengerId);
                reservedBaggageDto.LastName = passenger.LastName;
                reservedBaggageDto.BaggageWeight = baggageDetail.BaggageWeight;

                reservedBaggageDtos.Add(reservedBaggageDto);
            }
            return reservedBaggageDtos;

        }
        ///<summary>
        ///This function is used to get no of passengers under the booking and return integer
        ///<summary/>
        public async Task<int> GetNoOfPassengersForBaggageAsync(Booking booking)
        {
            return booking.NoOfPassengers;
        }
        ///<summary>
        ///This function is used to book baggage for passengers and return status of it in boolean
        ///<summary/>
        public async Task<bool> BaggageBooking(Booking booking,BaggageBookingDto baggageBookingDtos)
        {
            var checkins = await _context.checkIns.Where(x=>x.BookingId == booking.BookingId).FirstOrDefaultAsync();
            if(checkins == null)
            {
                return false;
            }
            List<Passenger> passengers = await GetPassengerDetails(booking.BookingId);
            var emptyBaggage = baggageBookingDtos.BaggageWeight.Where(x => x == 0).ToList();
            if(emptyBaggage.Count == 0)
            {
                if(passengers.Count > 0)
                {
                    for(int i =0 ; i<baggageBookingDtos.BaggageWeight.Count;i++)
                    {
                        Baggage baggage = new Baggage();
                        baggage.BagTag= PrintBagTag();
                        baggage.BaggageWeight=baggageBookingDtos.BaggageWeight[i];
                        baggage.BookingId= booking.BookingId;
                        baggage.PassengerId= passengers[i].PassengerId;
                        await _context.baggages.AddAsync(baggage);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;   
        }
        ///<summary>
        ///This function is used to get bagtag details under the booking and return list of bagtagexport dtos
        ///<summary/>
        public async Task<List<BagTagExport>> GetBagTagAsync(Booking booking)
        {
            List<BagTagExport> bagTagExports = new List<BagTagExport>();
            var baggageDetails = await _context.baggages.Where(x=>x.BookingId == booking.BookingId).ToListAsync();
            if(baggageDetails.Count > 0)
            {
                foreach(var baggageDetail in baggageDetails)
                {
                    BagTagExport bagTagExport = new BagTagExport();
                    bagTagExport.FromLocation = booking.SourceCity;
                    bagTagExport.ToLocation = booking.DestinationCity;
                    bagTagExport.FlightNumber = booking.FlightNumber;
                    bagTagExport.FirstName = GetPassengerById(baggageDetail.PassengerId).FirstName;
                    bagTagExport.LastName = GetPassengerById(baggageDetail.PassengerId).LastName;
                    bagTagExport.BagTag = baggageDetail.BagTag;
                    bagTagExport.Weight = baggageDetail.BaggageWeight.ToString()+"KG";
                    bagTagExport.DepartureDate = DateTime.ParseExact(booking.TravelDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToShortDateString();
                    bagTagExport.ArivalDate = DateTime.ParseExact(booking.TravelDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture).AddDays(1).ToShortDateString();


                    bagTagExports.Add(bagTagExport);

                }
                return bagTagExports;
            }
            return bagTagExports;
        }

        ///<summary>
        ///This function is used to get baggage classes
        ///<summary/>
        public async Task<List<BaggageWeightClassExport>> GetBaggageWeightClassAsync()
        {
            var baggageWeightClass = await _context.baggageWeightClasses.ToListAsync();

            return _mapper.Map<List<BaggageWeightClassExport>>(baggageWeightClass);
            
        }
        
    }
}