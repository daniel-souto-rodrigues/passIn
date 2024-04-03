namespace PassIn.Application.UseCases.Attendees.Register
{
    public class ResponseRegisteredAttendeeJson
    {
        public Guid Id { get; set; }

        public ResponseRegisteredAttendeeJson(Guid id)
        {
            Id = id;
        }
    }
}