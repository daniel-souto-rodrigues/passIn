using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestEventJson request)
    {
        var useCases = new RegisterEventUseCases();
        var response = useCases.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid eventId)
    {
        var useCase = new GetEventByIdUseCases();
        var response = useCase.Execute(eventId);

        return Ok(response);
    }

    [HttpPost("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseRegisteredAttendeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult Register([FromBody] RequestRegisterEventJson request,
        [FromRoute] Guid eventId)
    {
        var useCase = new RegisterAttendeesOnEventUseCase();

        var response = useCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }
}
