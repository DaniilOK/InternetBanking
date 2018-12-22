using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum CreateBankCardResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("User not found")]
        UserNotFound,

        [StringValue("Bank account not found")]
        BankAccountNotFound
    }
}
