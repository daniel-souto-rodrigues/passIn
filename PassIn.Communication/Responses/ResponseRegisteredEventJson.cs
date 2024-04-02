namespace PassIn.Communication.Responses
{
    public class ResponseRegisteredEventJson
    {
        public Guid Id { get; set; }

        public ResponseRegisteredEventJson(Guid id)
        {
            Id = id;
        }
    }
}
