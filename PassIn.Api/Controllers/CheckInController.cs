using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.DoAttendeeCheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        [HttpPost("{attendeeId}")]
        [ProducesResponseType(typeof(ResponseRegisteredCheckInJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult CheckIn([FromRoute] Guid attendeeId)
        {
            var useCase = new DoAttendeeCheckInUseCases();

            var response = useCase.Execute(attendeeId);
            
            return Created(string.Empty, response);
        }
    }
}
