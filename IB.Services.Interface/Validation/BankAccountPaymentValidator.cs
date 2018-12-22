using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class BankAccountPaymentValidator : AbstractValidator<BankAccountPaymentCommand>
    {
        public BankAccountPaymentValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
