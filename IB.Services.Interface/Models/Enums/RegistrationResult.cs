    using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum RegistrationResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("Login already exists")]
        LoginExists,

        [StringValue("Email already exists")]
        EmailExists
    }
}
