using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
