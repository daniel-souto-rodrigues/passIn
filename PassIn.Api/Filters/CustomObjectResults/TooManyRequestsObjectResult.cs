using Microsoft.AspNetCore.Mvc;

namespace PassIn.Api.Filters.CustomObjectResults
{
    public class TooManyRequestsObjectResult : ObjectResult
    {
        public TooManyRequestsObjectResult(object? value) : base(value)
        {

        }
    }
}