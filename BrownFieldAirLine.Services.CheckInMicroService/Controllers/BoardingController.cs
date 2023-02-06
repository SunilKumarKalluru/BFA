using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Repository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrownFieldAirLine.Services.CheckInMicroService.Controllers
{
    ///<summary>
    ///This controller class holds endpoints related to boarding
    ///expect information related to boarding pass
    ///<summary/>
     [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("Api/[controller]")]
    public class BoardingController : ControllerBase
    {
        private readonly ICheckInRepository _checkInRepository;
        private readonly ILogger<BoardingController> _logger;
        private readonly IBoardingRepository _boardingRepository;
        ///<summary>
        ///Constructor method that takes repositories and logger as Dependency Injection
        ///
        ///<summary/>
        public BoardingController(ILogger<BoardingController> logger,ICheckInRepository checkInRepository,IBoardingRepository boardingRepository)
        {
            _checkInRepository=checkInRepository;
            _boardingRepository=boardingRepository;
            _logger=logger;
        }
        /// <summary>
        /// Get boarding passes of passengers under same PNR
        /// </summary>
        [Route("BoardingPass")]
        [HttpGet]
        [ProducesResponseType(typeof(List<BoardingPassDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBoardingPass([FromQuery, BindRequired]string PNR)
        {
            try{
                ResponseMessage responseMessage = new ResponseMessage();
                var bookingDetails = await _checkInRepository.GetBookingByIdAsync(PNR);
                if(bookingDetails == null)
                {
                    responseMessage.Message="Booking Not Found";
                    return NotFound(responseMessage);
                }
                var boardingpass = await _boardingRepository.GetBoardingPass(bookingDetails);
                if(boardingpass == null)
                {
                    responseMessage.Message="Something went wrong";
                    return BadRequest(responseMessage);
                }
                return Ok(boardingpass);
            }
            catch (Exception ex)
            {   ResponseMessage responseMessage = new ResponseMessage();
                responseMessage.Message="Try Again";
                _logger.Log(LogLevel.Error,ex,"Error occured while passenger tried to get Boarding Pass");
                return BadRequest(responseMessage);
            }
            
        }
    }
}