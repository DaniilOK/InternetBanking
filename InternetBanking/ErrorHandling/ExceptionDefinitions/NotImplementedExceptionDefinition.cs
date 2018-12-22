using System;
using System.Net;

namespace InternetBanking.ErrorHandling.ExceptionDefinitions
{
    public class NotImplementedExceptionDefinition : ExceptionDefinition<NotImplementedException>
    {
        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotImplemented;
        }

        public override ErrorCode GetErrorCode()
        {
            return ErrorCode.NotImplemented;
        }
    }
}
