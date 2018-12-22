using IB.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace InternetBanking.Filters
{
    public class CommitRequired : TypeFilterAttribute
    {
        public CommitRequired() : base(typeof(CommitRequiredImplementation))
        {
        }

        private class CommitRequiredImplementation : ActionFilterAttribute
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger _logger;

            public CommitRequiredImplementation(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
            {
                _unitOfWork = unitOfWork;
                _logger = loggerFactory.CreateLogger("Unit of Work");
            }

            public override void OnActionExecuted(ActionExecutedContext context)
            {
                if (context.Exception != null)
                {
                    _logger.LogWarning("Cannot commit changes because of {@Exception}", context.Exception);
                    return;
                }

                if (!context.ModelState.IsValid)
                {
                    _logger.LogWarning("Cannot commit changes because ModelState is invalid");
                    return;
                }

                _unitOfWork.Commit(null);
            }
        }
    }
}
