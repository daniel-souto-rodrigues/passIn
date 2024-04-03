using PassIn.Api.Filters;
using PassIn.Communication.Requests;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Attendees.Register
{
    public class RegisterAttendeesOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;

        public RegisterAttendeesOnEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredAttendeeJson Execute(Guid eventId, RequestRegisterEventJson request)
        {
            Validate(eventId, request);

            var entity = new Infrastructure.Entities.Attendee
            {
                Event_Id = eventId,
                Email = request.Email,
                Name = request.Name,
                Created_At = DateTime.UtcNow
            };

            _dbContext.Attendees.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredAttendeeJson(entity.Id);
        }

        private void Validate(Guid eventId, RequestRegisterEventJson request)
        {
            var entityEvent = _dbContext.Events.Find(eventId);

            if (entityEvent is null)
                throw new NotFoundException("Event not found");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ErrorOnValidationException("The name must contain value");

            if (!EmailIsValid(request.Email))
                throw new ErrorOnValidationException("The e-mail is invalid");

            var attendeeAlreadyRegistered = _dbContext
                .Attendees.Any(at => at
                .Email.Equals(request.Email) && at
                .Event_Id.Equals(eventId));

            if (attendeeAlreadyRegistered)
                throw new ConflictException("You cannot register in the same event twice");

            var attendeesCount = _dbContext.Attendees.Count(at => at.Event_Id.Equals(eventId));

            if (attendeesCount >= entityEvent.Maximum_Attendees)
                throw new TooManyRegisterException("Event is full");
        }

        private bool EmailIsValid(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
