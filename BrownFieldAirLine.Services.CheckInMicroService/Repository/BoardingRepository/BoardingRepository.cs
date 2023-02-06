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

namespace BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository
{
    ///<summary>
    ///This class holds all functions related to boarding that are exposed in boarding controller and helper functions of it
    ///<summary/>
    public class BoardingRepository : IBoardingRepository
    {
        private readonly BrownFieldAirLineContext _context;

        private readonly IMapper _mapper;
        ///<summary>
        ///This constructor has parameters of context class and mapper for dependency injection
        ///<summary/>
        public BoardingRepository(BrownFieldAirLineContext context,IMapper mapper)
        {
            _context=context; 
            _mapper=mapper;  
        }
        ///<summary>
        ///This function is used to get seat no  by passenger id and booking id and returns passenger model
        ///<summary/>

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
        ///This function is used to get boarding passes for the booking
        ///<summary/>
        public async Task<List<BoardingPassDto>> GetBoardingPass(Booking booking)
        {
            var checkedInDetails = await _context.checkIns.Where(x=>x.BookingId == booking.BookingId && x.CheckInStatus == true).FirstOrDefaultAsync();
            if(checkedInDetails == null)
            {
                return null;
            }
            List<BoardingPassDto> boardingPasses = new List<BoardingPassDto>();
            List<Boarding> boardings = new List<Boarding>();
            BoardingPassDto boarding;
            Boarding boardingDetails;
            var boardingOfPassengers = await _context.boardings.Where(x=>x.BookingId == booking.BookingId).ToListAsync();
            if(boardingOfPassengers.Count == 0)
            {
                var passengers = await _context.passengers.Where(x=>x.BookingId == booking.BookingId).ToListAsync();
                foreach(var passenger in passengers)
                {
                    boardingDetails = new Boarding();
                    boardingDetails.BookingId = booking.BookingId;
                    boardingDetails.SeatNumber = await GetSeatNo((int)passenger.BookingId,passenger.PassengerId);
                    boardingDetails.PassengerId = passenger.PassengerId;
                    boardingDetails.BoardingTime = DateTime.Now.ToString();
                    boardingDetails.DepartureTime = DateTime.Now.AddMinutes(45).ToString();
                    boardingDetails.FromLocation = booking.SourceCity;
                    boardingDetails.ToLocation = booking.DestinationCity;
                    boardingDetails.FlightNumber = booking.FlightNumber;
                    boardings.Add(boardingDetails);

                    boarding = new BoardingPassDto();
                    boarding.FirstName = passenger.FirstName;
                    boarding.LastName=passenger.LastName;
                    boarding.BoardingTime = DateTime.Parse(boardingDetails.BoardingTime).ToShortTimeString();
                    boarding.DepartureTime = DateTime.Parse(boardingDetails.DepartureTime).ToShortTimeString();
                    boarding.DepartureDate = DateTime.Parse(boardingDetails.DepartureTime).ToShortDateString();
                    boarding.ArivalDate = DateTime.Parse(boardingDetails.DepartureTime).AddDays(1).ToShortDateString();
                    boarding.FromLocation = boardingDetails.FromLocation;
                    boarding.SeatNumber=boardingDetails.SeatNumber;
                    boarding.ToLocation = boardingDetails.ToLocation;
                    boarding.FlightNumber = booking.FlightNumber;
                    boardingPasses.Add(boarding);
                }
                await _context.boardings.AddRangeAsync(boardings);
                await _context.SaveChangesAsync();
                return boardingPasses;
            }
            else
            {
                var boardingPassDetails = await _context.boardings.Where(x=>x.BookingId == booking.BookingId).ToListAsync();
                foreach(var boardingPassDetail in boardingPassDetails)
                {
                    boarding = new BoardingPassDto();
                    boarding.FirstName = GetPassengerById(boardingPassDetail.PassengerId).FirstName;
                    boarding.LastName=GetPassengerById(boardingPassDetail.PassengerId).LastName;
                    boarding.BoardingTime = DateTime.Parse(boardingPassDetail.BoardingTime).ToString("HH:mm:ss");
                    boarding.DepartureTime = DateTime.Parse( boardingPassDetail.DepartureTime).ToShortTimeString();
                    boarding.DepartureDate = DateTime.Parse(boardingPassDetail.DepartureTime).ToShortDateString();
                    boarding.ArivalDate = DateTime.Parse(boardingPassDetail.DepartureTime).AddDays(1).ToShortDateString();
                    boarding.FromLocation = booking.SourceCity;
                    boarding.SeatNumber=await GetSeatNo((int)booking.BookingId,boardingPassDetail.PassengerId);
                    boarding.ToLocation = booking.DestinationCity;
                    boarding.FlightNumber = booking.FlightNumber;
                    boardingPasses.Add(boarding);
                }
                return boardingPasses;
            }

            
        }
        
    }
}