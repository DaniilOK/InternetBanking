using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum CreateAccountResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("User not found")]
        UserNotFound
    }
}
