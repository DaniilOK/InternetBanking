using System;
using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Models
{
    public class BankAccountModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public long Number { get; set; }
        public decimal Money { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public BankAccountModel(Guid id, Guid userId, long number, decimal money, DateTime endDate, bool active)
        {
            Id = id;
            UserId = userId;
            Number = number;
            Money = money;
            EndDate = endDate;
            Active = active;
        }
    }
}
