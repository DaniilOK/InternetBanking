using System;

namespace IB.Services.Interface.Commands
{
    public class BankAccountPaymentCommand : BaseCommand
    {
        public Guid BankAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
