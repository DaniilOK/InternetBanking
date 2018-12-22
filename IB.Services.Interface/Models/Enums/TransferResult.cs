using IB.Common;

namespace IB.Services.Interface.Models.Enums
{
    public enum TransferResult
    {
        [StringValue("Success")]
        Success,

        [StringValue("From card not found")]
        FromNotFound,

        [StringValue("To card not found")]
        ToNotFound,

        [StringValue("Not enough money")]
        NotEnoughMoney
    }
}
