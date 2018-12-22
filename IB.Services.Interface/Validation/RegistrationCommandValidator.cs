using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email is invalid");
            RuleFor(x => x.Login).NotEmpty().MinimumLength(4).MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(100);
        }
    }
}
