using IB.Repository.Interface.Models;
using IB.Services.Interface.Models;

namespace IB.Services.Mapping
{
    public static class BankCardMapping
    {
        public static CardModel ToCardModel(this BankCard bankCard)
        {
            return new CardModel(bankCard.Id, bankCard.UserId, bankCard.BankAccountId, bankCard.Number, bankCard.Validity, bankCard.Active);
        }
    }
}
