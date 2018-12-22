using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Models
{
    public class RegistrationModel
    {
        public RegistrationResult RegistrationResult { get; }

        public RegistrationModel(RegistrationResult registrationResult)
        {
            RegistrationResult = registrationResult;
        }
    }
}
