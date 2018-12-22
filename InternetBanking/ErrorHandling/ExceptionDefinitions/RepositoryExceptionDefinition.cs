using IB.Repository.Interface.Exceptions;

namespace InternetBanking.ErrorHandling.ExceptionDefinitions
{
    public class RepositoryExceptionDefinition : ExceptionDefinition<RepositoryException>
    {
        public override ErrorCode GetErrorCode()
        {
            return ErrorCode.AppService;
        }
    }
}
