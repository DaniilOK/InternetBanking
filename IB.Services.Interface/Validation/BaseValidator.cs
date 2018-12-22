using System;
using FluentValidation;
using IB.Services.Interface.Commands;

namespace IB.Services.Interface.Validation
{
    public class BaseValidator : AbstractValidator<BaseCommand>
    {
        public static readonly DateTime BaseDate = new DateTime(2000, 1, 1);
        public static readonly DateTime UltimateDate = new DateTime(2100, 1, 1);
    }
}
