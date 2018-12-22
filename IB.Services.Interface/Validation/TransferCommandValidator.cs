using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class TransferCommandValidator : AbstractValidator<TransferCommand>
    {
        public TransferCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
