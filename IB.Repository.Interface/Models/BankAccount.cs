using System;
using IB.Common.Helpers;

namespace IB.Repository.Interface.Models
{
    public class BankAccount : Entity<Guid>
    {
        public const int NumberLength = 18;

        public Guid UserId { get; set; }
        public long Number { get; set; }
        public decimal Money { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public User User { get; set; }

        public static BankAccount CreateBankAccount(Guid userId)
        {
            return new BankAccount()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Number = GenerateHelper.GenerateAccountNumber(),
                Money = 0,
                EndDate = DateTime.Now.AddYears(2),
                Active = true
            };
        }
    }
}
