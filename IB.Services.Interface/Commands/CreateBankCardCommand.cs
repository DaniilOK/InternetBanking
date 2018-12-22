using System;

namespace IB.Services.Interface.Commands
{
    public class CreateBankCardCommand
    {
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
    }
}
