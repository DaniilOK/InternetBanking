using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum PaymentResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("Card not found")]
        CardNotFound,

        [StringValue("Bank account not found")]
        BankAccountNotFound,

        [StringValue("Not enough money")]
        NotEnoughMoney
    }
}
