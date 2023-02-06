using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository
{
    ///<summary>
    ///This class holds all functions related to checkin that are exposed in checkin controller and helper functions of it
    ///<summary/>
    public class CheckInRepository : ICheckInRepository
    {
        private readonly BrownFieldAirLineContext _context;

        private readonly IMapper _mapper;
        ///<summary>
        ///This constructor has parameters of context class and mapper for dependency injection
        ///<summary/>
        public CheckInRepository(BrownFieldAirLineContext context,IMapper mapper)
        {
            _context=context; 
            _mapper=mapper;  
        }
        private async Task<bool> CheckBooking(int bookingId)
        {
            var bookingDetails = await _context.bookings.Where(x => x.BookingId == bookingId).FirstOrDefaultAsync();
            if(bookingDetails != null)
            {
                return true;
            }
            return false;
        }
        ///<summary>
        ///This function is used to get bookings for the PNR
        ///<summary/>
        public async Task<Booking> GetBookingByIdAsync(string pnrNo)
        {
           var bookingDetails = await _context.bookings.Where(x => x.PnrNo.ToLower() == pnrNo.ToLower()).FirstOrDefaultAsync();
           if(bookingDetails != null)
           {
            return bookingDetails;
           }
           return null;

        }
        ///<summary>
        ///This function is used to get bookings for the PNR
        ///<summary/>
        public async Task<Booking> GetBookingByIdAsync(string PnrNo ,string Email)
        {
            var bookingDetails = await _context.bookings.Where(x => x.PnrNo.ToLower() == PnrNo.ToLower()).FirstOrDefaultAsync();
            if(bookingDetails != null)
            {
                var passengers = await _context.passengers.Where(x=>x.BookingId == bookingDetails.BookingId).ToListAsync();
                if(passengers != null)
                {
                    var user = passengers.Where(x=>x.Email.ToLower() == Email.ToLower()).FirstOrDefault();
                    if(user != null)
                    {
                        return bookingDetails;
                    }
                }
                
            }
            return null;
        }
       
        ///<summary>
        ///This function is used to get checkin details for the booking
        ///<summary/>
        public async Task<CheckIn> GetCheckIn(Booking booking)
        {
            var checkIn = await _context.checkIns.Where(x=>x.BookingId == booking.BookingId).FirstOrDefaultAsync();

            if(checkIn != null)
            {
                return checkIn;
            }

            return null;
        }
        ///<summary>
        ///This function is used to checkin a passenger with PNR
        ///<summary/>
        public async Task<bool> CheckIn(CheckInDto checkInDto)
        {
            var booking = await _context.bookings.Where(x=>x.PnrNo.ToLower() == checkInDto.PnrNo.ToLower()).FirstOrDefaultAsync();
            if(booking != null)
            {
                var passeger = await _context.passengers.Where(x => x.BookingId == booking.BookingId && x.Email.ToLower() ==checkInDto.Email.ToLower()).FirstOrDefaultAsync();
                if(passeger != null)
                {
                    TimeSpan timeSpan = DateTime.ParseExact(booking.TravelDate, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture) - DateTime.Now ;
                    
                    if( timeSpan.TotalHours <= 24 && timeSpan.TotalHours >=1)
                    {
                        CheckIn checkIn = new CheckIn();
                        checkIn.BookingId = (int)passeger.BookingId;
                        checkIn.PnrNo = booking.PnrNo;
                        checkIn.LastName = passeger.LastName;
                        checkIn.Email=passeger.Email;
                        checkIn.CheckInStatus = true;
                        checkIn.CheckInTime = DateTime.Now;

                        await _context.checkIns.AddAsync(checkIn);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    
                }
                return false;
            }
            return false;
        }
        ///<summary>
        ///This function is used to validate checkin status for the PNR and email
        ///<summary/>
        public async Task<CheckInValidationDto> ValidateCheckinStatusAsync(Booking booking)
        {
            CheckInValidationDto checkInValidationDto = new CheckInValidationDto();
                var checkInDetails = await _context.checkIns.Where(x =>x.BookingId == booking.BookingId).FirstOrDefaultAsync();
                if(checkInDetails != null)
                {
                    checkInValidationDto.checkinValidation =true;
                    return checkInValidationDto;
                }
                return checkInValidationDto;
        }

        ///<summary>
        ///This function is used to validate checkin details for the PNR and email
        ///<summary/>
        public async Task<CheckInValidationDto> ValidateCheckinAsync(string PnrNo,string Email)
        {
            
            CheckInValidationDto checkInValidationDto = new CheckInValidationDto();
            var bookingDetails = await _context.bookings.Where(x=>x.PnrNo.ToLower() == PnrNo.ToLower()).FirstOrDefaultAsync();

            if(bookingDetails != null)
            {
                var passengerDetails = await _context.passengers.Where(x=>x.BookingId == bookingDetails.BookingId).ToListAsync();
                foreach(var passenger in passengerDetails)
                {
                    if(passenger.Email == Email.ToLower())
                    {
                        checkInValidationDto.checkinValidation =true;
                        return checkInValidationDto;
                    }
                }
            }
            return checkInValidationDto;
        }
        private async Task<string> GetSeatNo(int BookingId,int passegerId)
        {
            var Seat = await _context.seatings.Where(x=>x.BookingId ==BookingId && x.PassengerId == passegerId).FirstOrDefaultAsync();

            if(Seat != null)
            {
                return Seat.SeatNumber;
            }
            return null;
        }
        ///<summary>
        ///This function is used to get booking details for the respective PNR
        ///<summary/>
        public async Task<CheckedInBookingsDto> CheckedInBookingDetailsAsync(string PnrNo,string Email)
        {
            CheckedInBookingsDto checkedInBookingsDto = new CheckedInBookingsDto();

            var bookingDetails = await _context.bookings.Where(x=>x.PnrNo.ToLower() == PnrNo.ToLower()).FirstOrDefaultAsync();

            if(bookingDetails != null)
            {
                var passengerDetails = await _context.passengers.Where(x=>x.BookingId == bookingDetails.BookingId).ToListAsync();
                foreach(var passenger in passengerDetails)
                {
                    if(passenger.Email.ToLower() == Email.ToLower())
                    {
                        checkedInBookingsDto.SourceCity = bookingDetails.SourceCity;
                        checkedInBookingsDto.DestinationCity=bookingDetails.DestinationCity;
                        checkedInBookingsDto.FlightNumber=bookingDetails.FlightNumber;
                        checkedInBookingsDto.Email=passenger.Email;
                        checkedInBookingsDto.FirstName=passenger.FirstName;
                        checkedInBookingsDto.LastName=passenger.LastName;
                        checkedInBookingsDto.NumberOfPassengers=bookingDetails.NoOfPassengers;
                        checkedInBookingsDto.PnrNo=bookingDetails.PnrNo;
                        checkedInBookingsDto.TravelDate=bookingDetails.TravelDate;
                        checkedInBookingsDto.TravelClass =bookingDetails.ClassName.ToLower();
                        return checkedInBookingsDto;
                    }
                }
            }

            return null;
        }

    }
}