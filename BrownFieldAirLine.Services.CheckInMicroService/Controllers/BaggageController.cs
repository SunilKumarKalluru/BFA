using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Repository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrownFieldAirLine.Services.CheckInMicroService.Controllers
{
    /// <summary>
    /// Baggage service endpoints related to baggage
    /// </summary>
     [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("Api/[controller]")]
    public class BaggageController : ControllerBase
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly IBaggageRepository _baggageRepository;
        private readonly ILogger<BaggageController> _logger;
        /// <summary>
        /// Constructor method that takes repositories and logger as Dependency Injection
        /// </summary>
        public BaggageController(ICheckInRepository checkInRepository,IBaggageRepository baggageRepository,ILogger<BaggageController> logger)
        {
            _checkInRepository=checkInRepository;
            _baggageRepository=baggageRepository;
            _logger=logger;
        }
        /// <summary>
        /// Get passengers for the baggage
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
                var NoOfPassengers = await _baggageRepository.GetNoOfPassengersForBaggageAsync(bookingDetails);
                if(NoOfPassengers > 0)
                {
                    PassengerDetailsDto passengerDetailsDto = new PassengerDetailsDto();
                    passengerDetailsDto.NoOfPassengers = NoOfPassengers;
                    return Ok(passengerDetailsDto);
                }
                responseMessage.Message="Cant fetch No of Passengers";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching no of passengers");
                return BadRequest(responseMessage);
            }
            
        }
        /// <summary>
        /// Get validation of baggage whether it is booked or not
        /// </summary>
        [Route("ValidateBaggage")]
        [HttpGet]
        [ProducesResponseType(typeof(BaggageValidateExport),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateBaggage([FromQuery, BindRequired]string PNR)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var baggageDetails = await _baggageRepository.GetReservedBaggageAsync(bookingDetails);
                BaggageValidateExport baggageValidateExport = new BaggageValidateExport();
                if(baggageDetails.Count > 0)
                {
                    baggageValidateExport.baggageBookingStatus = true;
                    return Ok(baggageValidateExport);
                }
                return Ok(baggageValidateExport);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching baggage details under {pnrNo}");
                return BadRequest(responseMessage);
            }

        }
        /// <summary>
        /// Get reserved baggages for passenger
        /// </summary>
        [Route("GetBaggage")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservedBaggageDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservedBaggageDetails([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,Email);
                if(bookingDetails == null)
                {
                   responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var baggageDetails = await _baggageRepository.GetReservedBaggageAsync(bookingDetails);
                if(baggageDetails != null)
                {
                    return Ok(baggageDetails);
                }
                responseMessage.Message="Cant fetch reserved baggage details";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching baggage details under {pnrNo}");
                return BadRequest(responseMessage);
            }
            
        }
        /// <summary>
        /// Book baggage for passengers under same PNR
        /// </summary>
        [Route("BookBaggage")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BaggageBooking([FromQuery, BindRequired]string PNR,[FromBody]BaggageBookingDto baggageBookingDtos)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,baggageBookingDtos.Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var passengerDetails = await _baggageRepository.GetPassengerDetails(bookingDetails.BookingId);
                var emailFound = passengerDetails.Where(x=>x.Email == baggageBookingDtos.Email).FirstOrDefault();
                if(emailFound == null)
                {
                    responseMessage.Message="Email not Found";
                    return NotFound(responseMessage);
                }
                var baggageDetails = await _baggageRepository.GetBaggageAsync(bookingDetails.BookingId,baggageBookingDtos);
                if(baggageDetails.Count > 0)
                {
                    responseMessage.Message="Baggage already booked";
                    return BadRequest(responseMessage);
                }
                var baggages = await _baggageRepository.BaggageBooking(bookingDetails,baggageBookingDtos);
                if(baggages)
                {
                    responseMessage.Message = "BaggageBooked";
                    return Ok(responseMessage);
                }
                responseMessage.Message="Something went wrong";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
               ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching baggage details under {pnrNo}");
                return BadRequest(responseMessage);
            }
            
        }
        /// <summary>
        /// Get bagtags of baggage for passengers under same PNR
        /// </summary>
        [Route("PrintBagTag")]
        [HttpGet]
        [ProducesResponseType(typeof(List<BagTagExport>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBaggageTags([FromQuery, BindRequired]string PNR)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
            var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
            var baggageDetails = await _baggageRepository.GetBagTagAsync(bookingDetails);
            if(baggageDetails.Count == 0)
            {
                responseMessage.Message="BagTags Not Found";
                return NotFound(responseMessage);
            }
            return Ok(baggageDetails);
            }
            catch(Exception ex)
            {
               ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching baggage details under {pnrNo}");
                return BadRequest(responseMessage);
            }
            
        }
        /// <summary>
        /// Get all baggage weight in system that are available
        /// </summary>
        [Route("GetBaggageWeightClass")]
        [HttpGet]
        [ProducesResponseType(typeof(List<BaggageWeightClassExport>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBaggageWeights()
        {
            
            try{
                
                var baggageDetails = await _baggageRepository.GetBaggageWeightClassAsync();
                return Ok(baggageDetails);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message = "Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching baggage weight classes");
                return BadRequest(responseMessage);
            }
        }
    }
}