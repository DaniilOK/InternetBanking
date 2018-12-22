using System;

namespace IB.Services.Interface.Commands
{
    public class TransferCommand : BaseCommand
    {
        public Guid FromCardId { get; set; }

        public Guid ToCardId { get; set; }

        public decimal Amount { get; set; }
    }
}
