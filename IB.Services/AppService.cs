using FluentValidation;
using IB.Common;
using IB.Repository.Interface;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Validation;

namespace IB.Services
{
    public abstract class AppService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected IValidatorFactory ValidatorFactory { get; }

        protected AppService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory)
        {
            Expect.ArgumentNotNull(unitOfWork, nameof(unitOfWork));

            UnitOfWork = unitOfWork;
            ValidatorFactory = validatorFactory ?? new ValidatorFactory();
        }

        protected void EnsureIsValid<T>(T command) where T : BaseCommand
        {
            Expect.ArgumentNotNull(command, nameof(command));

            var validator = ValidatorFactory.GetValidator<T>();
            validator?.ValidateAndThrow(command);
        }
    }
}
