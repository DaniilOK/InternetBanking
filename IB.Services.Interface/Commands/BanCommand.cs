using System;

namespace IB.Services.Interface.Commands
{
    public class BanCommand : BaseCommand
    {
        public Guid Id { get; set; }
        public bool IsBan { get; set; }
    }
}
