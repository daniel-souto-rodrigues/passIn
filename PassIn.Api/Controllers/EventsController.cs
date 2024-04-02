using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

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
        try
        {
            var useCases = new RegisterEventUseCases();
            var response = useCases.Execute(request);

            return Created(string.Empty, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ResponseErrorJson(ex.Message));
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Unknown Error"));
        }
    }

    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid eventId)
    {
        try
        {
            var useCase = new GetEventByIdUseCases();
            var response = useCase.Execute(eventId);
            return Ok(response);
        }
        catch (PassInException ex)
        {
            return NotFound(new ResponseErrorJson(ex.Message));
        }
    }
}
