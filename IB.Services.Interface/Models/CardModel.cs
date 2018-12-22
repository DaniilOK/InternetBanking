using System;

namespace IB.Services.Interface.Models
{
    public class CardModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BankAccountId { get; set; }
        public long Number { get; set; }
        public DateTime Validity { get; set; }
        public bool Active { get; set; }

        public CardModel(Guid id, Guid userId, Guid bankAccountId, long number, DateTime validity, bool active)
        {
            Id = id;
            UserId = userId;
            BankAccountId = bankAccountId;
            Number = number;
            Validity = validity;
            Active = active;
        }
    }
}
