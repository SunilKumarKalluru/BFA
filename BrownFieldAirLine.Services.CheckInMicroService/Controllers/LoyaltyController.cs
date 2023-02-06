using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BrownFieldAirLine.Services.CheckInMicroService.Dtos.Export;
using BrownFieldAirLine.Services.CheckInMicroService.Repository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrownFieldAirLine.Services.CheckInMicroService.Controllers
{
    ///<summary>
    ///This controller class holds endpoints related to Loyalty
    ///expect information related to loyalty
    ///<summary/>
     [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("Api/[controller]")]
    public class LoyaltyController : ControllerBase
    {
       private readonly ILoyaltyRepository _loyaltyRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly ILogger<LoyaltyController> _logger;
        ///<summary>
        ///Constructor method that takes repositories and logger as Dependency Injection
        ///
        ///<summary/>
        public LoyaltyController(ILogger<LoyaltyController> logger,ILoyaltyRepository loyaltyRepository,ICheckInRepository checkInRepository)
        {
            _loyaltyRepository=loyaltyRepository;
            _checkInRepository=checkInRepository;
            _logger=logger;
        }
        /// <summary>
        /// Get and update loyalty points of passenger
        /// </summary>
        [Route("LoyaltyPoints")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseMessage),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLoylaty([FromQuery, BindRequired]Guid UserId, [FromQuery, BindRequired]string PNR)
        {
          try{
            ResponseMessage responseMessage = new ResponseMessage();
              var booking = await _checkInRepository.GetBookingByIdAsync(PNR);
              if(booking == null)
              {
                responseMessage.Message = "something went wrong";
                  return BadRequest(responseMessage);
              }
              var loyalty = await _loyaltyRepository.GetLoyalty(UserId, booking.BookingId);
              if(loyalty!= null)
              {
                responseMessage.Message = $"Current Loyalty Points :{loyalty.LoyaltyPoints}";
                return Ok(responseMessage);
              }
              responseMessage.Message = "Bad request";
              return BadRequest(responseMessage);
          }
          catch(Exception ex)
          {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.Message="Unable to update loyalty";
              _logger.LogError(ex.Message);
              return BadRequest(responseMessage);
          }
        }
        
    }
}