using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Repository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrownFieldAirLine.Services.CheckInMicroService.Controllers
{
    ///<summary>
    ///This controller class holds endpoints related to seating
    ///expect information related to seatbookings
    ///<summary/>
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("Api/[controller]")]
    public class SeatingController : ControllerBase
    {

        private readonly ISeatingRepository _seatingRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly ILogger<SeatingController> _logger;
        ///<summary>
        ///Constructor method that takes repositories and logger as Dependency Injection
        ///<summary/>
        public SeatingController(ILogger<SeatingController> logger,ISeatingRepository seatingRepository,ICheckInRepository checkInRepository)
        {
            _seatingRepository=seatingRepository;
            _checkInRepository=checkInRepository;
            _logger=logger;
        }
        /// <summary>
        /// Get no of passengers under PNR 
        /// </summary>
        [Route("FindPassengers")]
        [HttpGet]
        [ProducesResponseType(typeof(PassengerDetailsDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNoOfPassengersForSeating([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var NoOfPassengers = await _seatingRepository.GetNoOfPassengersForSeatingAsync(bookingDetails);
                if(NoOfPassengers > 0)
                {
                    PassengerDetailsDto passengerDetailsDto = new PassengerDetailsDto();
                    passengerDetailsDto.NoOfPassengers = NoOfPassengers;
                    return Ok(passengerDetailsDto);
                }
                responseMessage.Message="Cant fetch No of Passengers";
                return NotFound(responseMessage);
                
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try again";
                _logger.LogError(ex.Message);
                return BadRequest(responseMessage);
            }
        }
        /// <summary>
        /// Get seats booked under flight related to PNR
        /// </summary>
        [Route("SeatsBooked")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservedSeatsDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookedSeats([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var reservedSeats = await _seatingRepository.GetReservedSeatsAsync(bookingDetails);
                if(reservedSeats.Count > 0)
                {
                return Ok(reservedSeats);
                }
                responseMessage.Message="Cant fetch reserved Seats";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Unable to retrieve seats booked try after some time";
                _logger.LogError(ex.Message);
                return BadRequest(responseMessage);
            }
            
            
        }
        /// <summary>
        ///  Validate seating details for the PNR  
        /// </summary>
        [Route("ValidateSeating")]
        [HttpGet]
        [ProducesResponseType(typeof(SeatingValidateExport),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateSeating([FromQuery, BindRequired]string PNR)
        {
            try
            {
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR);
                if(bookingDetails == null)
                {
                     responseMessage.Message="Booking Not Found";
                    _logger.LogInformation($"user tried to fetch details using PNR : {PNR}");
                    return NotFound(responseMessage);
                }
                var seatingDetails = await _seatingRepository.GetBookedSeatAsync(bookingDetails.BookingId);
                SeatingValidateExport seatingValidateExport = new SeatingValidateExport();
                if(seatingDetails.Count > 0)
                {
                    seatingValidateExport.seatBookingStatus = true;
                    return Ok(seatingValidateExport);
                }
                else
                {
                    return Ok(seatingValidateExport);
                }
                
                
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Something went wrong";
                _logger.LogError(ex.Message);
                return BadRequest(responseMessage);
            }
        }
        /// <summary>
        ///  Get seat details of passengers under the PNR  
        /// </summary>
        [Route("SeatsBookedByUser")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservedSeatsDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SeatsBookedByUser([FromQuery, BindRequired]string PNR)
        {
            try
            {
                 ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    _logger.LogInformation($"user tried to fetch details using PNR : {PNR}");
                    return NotFound(responseMessage);
                }
                var seatingDetails = await _seatingRepository.GetBookedSeatByUserAsync(bookingDetails.BookingId);
                if(seatingDetails.Count > 0)
                {
                    return Ok(seatingDetails);
                }
                return Ok(seatingDetails);
            }
            catch(Exception ex)
            {
               ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Something went wrong";
                _logger.LogError(ex.Message);
                return BadRequest(responseMessage);
            }

        }
        /// <summary>
        ///  Book seats of passengers under the PNR  
        /// </summary>
        [Route("BookSeat")]
        [HttpPost]
        [ProducesResponseType(typeof(SeatingExportDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SeatBooking([FromQuery, BindRequired]string PNR,[FromBody] SeatBookingDto seatBookingDto)
        {
            try
            {
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,seatBookingDto.Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    _logger.LogInformation($"user tried to fetch details using PNR : {PNR}");
                    return NotFound(responseMessage);
                }
                var seatingDetails = await _seatingRepository.GetBookedSeatAsync(bookingDetails.BookingId);
                if(seatingDetails.Count > 0)
                {
                    responseMessage.Message="Seats already booked under this booking";
                    return BadRequest(responseMessage);
                }
                var checkedSeatsRequested = await _seatingRepository.CheckSeatsRequested(bookingDetails,seatBookingDto);
                if(checkedSeatsRequested.Count > 0)
                {
                    
                    string seatsReserved = String.Join(",",checkedSeatsRequested.Select(o=>o.SeatNumber.ToString()));
                    responseMessage.Message="Seats Already Reserved : "+seatsReserved;
                    return BadRequest(responseMessage);
                }
                var seatBookingStatus = await _seatingRepository.SeatBooking(bookingDetails,seatBookingDto);
                SeatingExportDto seatingExportDto= new SeatingExportDto();
                if(seatBookingStatus)
                {
                    List<string> seatsReserved = new List<string>();
                    foreach(var seat in seatBookingDto.SeatNumber)
                    {
                        seatsReserved.Add(seat);
                    }
                    seatingExportDto.SeatsReserved =seatsReserved;
                    _logger.LogInformation(seatingExportDto.SeatsReserved.ToString()+"reserved under pnr : {PNR}");
                    return Ok(seatingExportDto);
                }
                responseMessage.Message="Something went wrong";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Something went wrong";
                _logger.LogError(ex.Message.ToString());
                return BadRequest(responseMessage);
            }
            
        }
    }
}