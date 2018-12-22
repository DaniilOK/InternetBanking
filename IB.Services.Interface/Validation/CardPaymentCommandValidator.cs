using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class CardPaymentCommandValidator : AbstractValidator<CardPaymentCommand>
    {
        public CardPaymentCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
