namespace PassIn.Application.UseCases.Events.RegisterAttendee
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