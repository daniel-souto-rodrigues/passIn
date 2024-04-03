using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventId;
using PassIn.Application.UseCases.Attendees.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        [HttpPost("{eventId}/register")]
        [ProducesResponseType(typeof(ResponseRegisteredAttendeeJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult Register([FromBody] RequestRegisterEventJson request,
        [FromRoute] Guid eventId)
        {
            var useCase = new RegisterAttendeesOnEventUseCase();

            var response = useCase.Execute(eventId, request);

            return Created(string.Empty, response);
        }

        [HttpGet("{eventId}")]
        [ProducesResponseType(typeof(ResponseAllAttendeesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromRoute] Guid eventId)
        {
            var useCase = new GetAllAttendeesByEventIdUseCases();

            var response = useCase.Execute(eventId);

            return Ok(response);
        }
    }
}
