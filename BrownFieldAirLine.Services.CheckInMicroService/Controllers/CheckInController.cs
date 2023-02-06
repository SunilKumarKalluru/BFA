using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrownFieldAirLine.Services.CheckInMicroService.Controllers
{
    ///<summary>
    ///This controller class holds endpoints related to checkin
    ///expect information related to checkin and neccessary validations
    ///<summary/>
     [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("Api/[controller]")]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly ILogger<CheckInController> _logger;
        ///<summary>
        ///Constructor method that takes repositories and logger as Dependency Injection
        ///
        ///<summary/>
        public CheckInController(ILogger<CheckInController> logger,ICheckInRepository checkInRepository)
        {
            _checkInRepository=checkInRepository;
            _logger=logger;
        }
        /// <summary>
        /// Post passenger web check-in status
        /// </summary>
        [Route("CheckIn")]
        [HttpPost]
        [ProducesResponseType(typeof(CheckinExportDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CheckInPassenger([FromBody] CheckInDto checkInDto)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(checkInDto.PnrNo,checkInDto.Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var CheckInRecords = await _checkInRepository.GetCheckIn(bookingDetails);
                if(CheckInRecords != null)
                {
                    responseMessage.Message="CheckIn already done for this booking";
                    _logger.Log(LogLevel.Information,"Checkin Already Completed for user with pnr : {checkInDto.PnrNo}");
                    return BadRequest(responseMessage);
                } 
                var CheckInStatus = await _checkInRepository.CheckIn(checkInDto);
                CheckinExportDto checkinExportDto = new CheckinExportDto();
                if(CheckInStatus)
                {
                    checkinExportDto.CheckinStatus =true;
                    _logger.Log(LogLevel.Information,"Checkin Completed for user with pnr : {checkInDto.PnrNo}");
                    return Ok(checkinExportDto);
                }
                return Ok(checkinExportDto);
            }
            catch(Exception ex)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while passenger tried to checkin");
                return BadRequest(responseMessage);
            }
        }
        /// <summary>
        /// Get passenger web check-in status
        /// </summary>
        [Route("CheckInStatus")]
        [HttpGet]
        [ProducesResponseType(typeof(CheckInValidationDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ValidateCheckinStatus([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR,Email);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var checkInValidationDetails = await _checkInRepository.ValidateCheckinStatusAsync(bookingDetails);
                if(checkInValidationDetails.checkinValidation)
                {
                    return Ok(checkInValidationDetails);
                }
                return Ok(checkInValidationDetails);
                
            }
            catch(Exception ex)
            {
                 ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while validating passenger details");
                return BadRequest(responseMessage); 
            }
            
        }
        /// <summary>
        /// Get validation on PNR and Email of passenger
        /// </summary>
        [Route("ValidateCheckIn")]
        [HttpGet]
        [ProducesResponseType(typeof(CheckInValidationDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateCheckin([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                var checkInValidationDetails = await _checkInRepository.ValidateCheckinAsync(PNR,Email);
                if(checkInValidationDetails.checkinValidation)
                {
                    return Ok(checkInValidationDetails);
                }
                return Ok(checkInValidationDetails);
            }
            catch(Exception ex)
            {
                 ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while validating passenger details");
                return BadRequest(responseMessage); 
            }
            
        }
        /// <summary>
        /// Get booking details of passenger
        /// </summary>
        [Route("BookingDetails")]
        [HttpGet]
        [ProducesResponseType(typeof(CheckedInBookingsDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCheckedInDetails([FromQuery, BindRequired]string PNR,[FromQuery, BindRequired]string Email)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                
                var checkedInBookings = await _checkInRepository.CheckedInBookingDetailsAsync(PNR,Email);
                if(checkedInBookings != null)
                {
                    return Ok(checkedInBookings);
                }
                responseMessage.Message="something went wrong";
                return BadRequest(responseMessage);
            }
            catch(Exception ex)
            {
                
                 ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while fetching passenger details");
                return BadRequest(responseMessage); 
            }
            
        }

    }
}
