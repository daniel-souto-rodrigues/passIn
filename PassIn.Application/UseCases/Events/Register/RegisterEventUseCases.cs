using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCases
    {
        private readonly PassInDbContext _dbContext;

        public RegisterEventUseCases()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredEventJson Execute(RequestEventJson request)
        {
            Validate(request);

            var entity = new Infrastructure.Entities.Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-"),
            };

            _dbContext.Events.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredEventJson(entity.Id); 
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new ErrorOnValidationException("The maximum Attendees is invalid.");
            }

            if(string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ErrorOnValidationException("The tittle must contain value");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("The details must contain value");
            }
        }
    }
}
