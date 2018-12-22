using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum LoginResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("Incorrect login")]
        IncorrectLogin,

        [StringValue("Incorrect password")]
        IncorrectPassword,

        [StringValue("User is inactive")]
        UserIsInactive,

        [StringValue("User is blocked")]
        UserIsBlocked,

        [StringValue("User is not in the app")]
        ExternalUser
    }
}
