using System;

namespace IB.Services.Interface.Commands
{
    public class CardPaymentCommand : BaseCommand
    {
        public Guid CardId { get; set; }
        public decimal Amount { get; set; }
    }
}
