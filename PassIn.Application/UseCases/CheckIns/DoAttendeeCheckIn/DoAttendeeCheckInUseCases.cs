using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.CheckIns.DoAttendeeCheckIn
{
    public class DoAttendeeCheckInUseCases
    {
        private readonly PassInDbContext _dbContext;

        public DoAttendeeCheckInUseCases()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredCheckInJson Execute(Guid attendeeId)
        {
            Validate(attendeeId);

            var entity = new Infrastructure.Entities.CheckIn
            {
                Attendee_Id = attendeeId,
                Created_At = DateTime.UtcNow
            };

            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredCheckInJson(entity.Id, entity.Created_At);
        }

        private void Validate(Guid attendeeId)
        {
            var existAttendee = _dbContext.Attendees.Any(at => at.Id.Equals(attendeeId));

            if (existAttendee == false)
                throw new NotFoundException("Attendee not found");

            var existCheckIns = _dbContext.CheckIns.Any(ch => ch.Attendee_Id.Equals(attendeeId));

            if (existCheckIns)
                throw new ConflictException("this attendee already made checkIn");
        }
    }
}
