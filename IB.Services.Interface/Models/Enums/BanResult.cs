using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum BanResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("User not found")]
        NotFound
    }
}
