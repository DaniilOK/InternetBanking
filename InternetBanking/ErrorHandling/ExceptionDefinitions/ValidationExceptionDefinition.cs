using System.Net;
using FluentValidation;

namespace InternetBanking.ErrorHandling.ExceptionDefinitions
{
    public class ValidationExceptionDefinition : ExceptionDefinition<ValidationException>
    {
        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }

        public override ErrorCode GetErrorCode()
        {
            return ErrorCode.Validation;
        }
    }
}
