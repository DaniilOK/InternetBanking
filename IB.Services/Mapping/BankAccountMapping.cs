using IB.Repository.Interface.Models;
using IB.Services.Interface.Models;

namespace IB.Services.Mapping
{
    public static class BankAccountMapping
    {
        public static BankAccountModel ToBankAccountModel(this BankAccount bankAccount)
        {
            return new BankAccountModel(bankAccount.Id, bankAccount.UserId, bankAccount.Number, bankAccount.Money, 
                bankAccount.EndDate, bankAccount.Active);
        }
    }
}
