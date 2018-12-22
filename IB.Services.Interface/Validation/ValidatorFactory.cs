using System;
using System.Collections.Generic;
using FluentValidation;

namespace IB.Services.Interface.Validation
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static readonly Dictionary<Type, IValidator> Validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            foreach (var scanResult in AssemblyScanner.FindValidatorsInAssemblyContaining<BaseValidator>())
            {
                var interfaceType = scanResult.InterfaceType;
                var validatorType = scanResult.ValidatorType;
                if (!validatorType.IsGenericType)
                {
                    Validators.Add(interfaceType, (IValidator)Activator.CreateInstance(validatorType));
                }
            }
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return Validators.TryGetValue(validatorType, out var validator) ? validator : null;
        }
    }
}
