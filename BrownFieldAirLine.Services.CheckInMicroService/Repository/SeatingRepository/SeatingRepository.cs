using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BrownFieldAirLine.Services.CheckInMicroService.Context;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository
{
    public class SeatingRepository : ISeatingRepository
    {
        ///<summary>
        ///This class holds all functions related to seating that are exposed in seating controller and helper functions of it
        ///<summary/>

        private readonly BrownFieldAirLineContext _context;

        private readonly IMapper _mapper;
        ///<summary>
        ///This constructor has parameters of context class and mapper for dependency injection
        ///<summary/>
        public SeatingRepository(BrownFieldAirLineContext context,IMapper mapper)
        {
            _context=context; 
            _mapper=mapper;  
        }
        private async Task<List<Passenger>> GetPassengerDetails(int BookingId)
        {
            List<Passenger> passengers = new List<Passenger>();
            var passenger =await _context.passengers.Where(x => x.BookingId == BookingId).ToListAsync();
            if(passenger.Count > 0)
            {
                passengers = passenger;
                return passengers;
            }
            return passengers;
        }
        ///<summary>
        ///This function is used to fetch seats booked under PNR  
        ///<summary/>
        public async Task<List<Seating>> GetBookedSeatAsync(int bookingId)
        {
            List<Seating> seatings = new List<Seating>();
            var seatingDetails = await _context.seatings.Where(x=>x.BookingId == bookingId).ToListAsync();
            if(seatingDetails.Count > 0)
            {
                seatings = seatingDetails;
                return seatings;
            }
            return seatings;
        }
        //<summary>
        ///This function is used to fetch seats booked under PNR  
        ///<summary/>
        public async Task<List<ReservedSeatsDto>> GetBookedSeatByUserAsync(int bookingId)
        {
            List<ReservedSeatsDto> reservedSeatsDtos = new List<ReservedSeatsDto>();
            var seatingDetails =  await _context.seatings.Where(x=>x.BookingId == bookingId).ToListAsync();
            if(seatingDetails.Count > 0)
            {
                foreach(var item in seatingDetails)
                {
                    ReservedSeatsDto reservedSeat = new ReservedSeatsDto();
                    reservedSeat.SeatNumber = item.SeatNumber;
                    reservedSeatsDtos.Add(reservedSeat);
                }
                return reservedSeatsDtos;
            }
            return reservedSeatsDtos;
        }
        ///<summary>
        ///This function is used to no of passengers under the booking
        ///<summary/>
        public async Task<int> GetNoOfPassengersForSeatingAsync(Booking booking)
        {
            return booking.NoOfPassengers;
        }
        ///<summary>
        ///This function is used to check whether requested seats for passengers
        ///<summary/>
        public async Task<List<ReservedSeatsDto>> CheckSeatsRequested(Booking booking,SeatBookingDto seatBookingDto)
        {
            
            List<ReservedSeatsDto> reservedSeatsDtos = new List<ReservedSeatsDto>();
            var seatingDetails = await GetReservedSeatsAsync(booking);
            if(seatingDetails == null)
            {
                return reservedSeatsDtos;
            }
            foreach(var seat in seatingDetails)
            {
                var seatBooked = seatBookingDto.SeatNumber.Where(x=>x == seat.SeatNumber).FirstOrDefault();
                if(seatBooked == "" || seatBooked == null)
                {
                    continue;
                }
                else{
                    ReservedSeatsDto reservedSeatsDto = new ReservedSeatsDto();
                    reservedSeatsDto.SeatNumber = seat.SeatNumber;

                    reservedSeatsDtos.Add(reservedSeatsDto);
                }
            }
            return reservedSeatsDtos;

        }
        ///<summary>
        ///This function is used to fetch seats booked under flight related to PNR  
        ///<summary/>
        public async Task<List<ReservedSeatsDto>> GetReservedSeatsAsync(Booking booking)
        {
            List<ReservedSeatsDto> reservedSeatsDtos = new List<ReservedSeatsDto>();
            var bookedSeats = await _context.seatings.Where(x=>x.FlightNumber == booking.FlightNumber).ToListAsync();

            if(bookedSeats != null)
            {
                foreach(var bookedSeat in bookedSeats)
                {
                    ReservedSeatsDto reservedSeatsDto = new ReservedSeatsDto();
                    reservedSeatsDto.SeatNumber = bookedSeat.SeatNumber;
                    reservedSeatsDtos.Add(reservedSeatsDto);
                }
                return reservedSeatsDtos;
            }
            return reservedSeatsDtos;
        }
        ///<summary>
        ///This function is used to book seats for the passengers
        ///<summary/>
        public async Task<bool> SeatBooking(Booking booking,SeatBookingDto seatBookingDto)
        {
            var checkins = await _context.checkIns.Where(x=>x.BookingId == booking.BookingId).FirstOrDefaultAsync();
            if(checkins == null)
            {
                return false;
            }
            List<Passenger> passengers = await GetPassengerDetails(booking.BookingId);
            if(passengers != null)
            {
                for(int i =0;i<seatBookingDto.SeatNumber.Count;i++)
                {
                    Seating seatingDetails = new Seating();
                    seatingDetails.SeatNumber=seatBookingDto.SeatNumber[i];
                    seatingDetails.FlightNumber=booking.FlightNumber; 
                    seatingDetails.BookingId = booking.BookingId;
                    seatingDetails.PassengerId = passengers[i].PassengerId;

                    await _context.seatings.AddAsync(seatingDetails);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}