using System;
using System.Net;

namespace InternetBanking.ErrorHandling.ExceptionDefinitions
{
    public class ExceptionDefinition
    {
        public Type ExceptionType { get; }

        public ExceptionDefinition(Type exceptionType)
        {
            ExceptionType = exceptionType;
        }

        public bool Matches(Type exceptionType)
        {
            return exceptionType == ExceptionType;
        }

        public ErrorResponse GetErrorResponse(Exception exception)
        {
            return new ErrorResponse
            {
                StatusCode = GetStatusCode(),
                ErrorCode = (int)GetErrorCode(),
                Message = GetMessage(exception)
            };
        }

        public virtual HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.InternalServerError;
        }

        public virtual ErrorCode GetErrorCode()
        {
            return ErrorCode.Default;
        }

        protected virtual string GetMessage(Exception exception)
        {
            return exception.Message;
        }
    }
}
