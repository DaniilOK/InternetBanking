using System;
using IB.Common.Helpers;

namespace IB.Repository.Interface.Models
{
    public class BankCard : Entity<Guid>
    {
        public const int NumberLength = 16;

        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public long Number { get; set; }
        public string VerificationCode { get; set; }
        public string PinCode { get; set; }
        public DateTime Validity { get; set; }
        public bool Active { get; set; }

        public User User { get; set; }
        public BankAccount BankAccount { get; set; }

        public static BankCard CreateNewBankCard(Guid userId, Guid bankAccount)
        {
            var verificationCode = GenerateHelper.GenerateVerificationCode();
            var pinCode = GenerateHelper.GeneratePinCode();

            return new BankCard()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                BankAccountId = bankAccount,
                Number = GenerateHelper.GenerateCardNumber(),
                VerificationCode = CalculateHashHelper.ComputeHash(verificationCode.ToString()),
                PinCode = CalculateHashHelper.ComputeHash(pinCode.ToString()),
                Validity = DateTime.Now.AddYears(2),
                Active = true
            };
        }
    }
}
