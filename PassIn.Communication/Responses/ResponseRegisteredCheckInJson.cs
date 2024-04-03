namespace PassIn.Communication.Responses
{
    public class ResponseRegisteredCheckInJson
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }

        public ResponseRegisteredCheckInJson(Guid id, DateTime createdAt)
        {
            Id = id;
            Created_At = createdAt;
        }
    }
}
