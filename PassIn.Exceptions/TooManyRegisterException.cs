using PassIn.Exceptions;

namespace PassIn.Api.Filters
{
    public class TooManyRegisterException : PassInException
    {
        public TooManyRegisterException(string message) : base(message)
        {

        }
    }
}