using System.Collections.Generic;
using System.Linq;
using IB.Common;
using InternetBanking.ErrorHandling;
using InternetBanking.ErrorHandling.ExceptionDefinitions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace InternetBanking.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly List<ExceptionDefinition> _exceptionDefinitions;

        public ExceptionFilter(ILoggerFactory loggerFactory)
        {
            Expect.ArgumentNotNull(loggerFactory, nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger("Global Exception Filter");
            _exceptionDefinitions = new List<ExceptionDefinition>
            {
                new ValidationExceptionDefinition(),
                new RepositoryExceptionDefinition(),
                new NotImplementedExceptionDefinition()
            };
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionType = exception.GetType();
            var definition = _exceptionDefinitions.FirstOrDefault(d => d.Matches(exceptionType));
            if (definition == null)
            {
                definition = new ExceptionDefinition(exceptionType);
                _exceptionDefinitions.Add(definition);
            }
            var response = definition.GetErrorResponse(exception);
            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode,
                DeclaredType = typeof(ErrorResponse)
            };
            _logger.LogError("Global Exception Filter: {@Exception}", context.Exception);
        }
    }
}
