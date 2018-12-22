using System;

namespace InternetBanking.ErrorHandling.ExceptionDefinitions
{
    public class ExceptionDefinition<TException> : ExceptionDefinition where TException : Exception
    {
        public ExceptionDefinition() : base(typeof(TException))
        {
        }

        public virtual string GetMessage(TException exception)
        {
            return exception.Message;
        }

        protected override string GetMessage(Exception exception)
        {
            return GetMessage((TException)exception);
        }
    }
}
