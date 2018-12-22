using System;
using System.Collections.Generic;
using IB.Services.Interface.Models;
using IB.Services.Interface.Models.Enums;

namespace IB.Services.Interface.Interfaces
{
    public interface IAccountService
    {
        BankAccountModel GetAccount(Guid accoutId);

        IReadOnlyCollection<BankAccountModel> GetAccounts(Guid userId);

        CardModel GetCard(Guid id);

        IReadOnlyCollection<CardModel> GetCards(Guid userId);

        CreateAccountResult CreateAccount(Guid userId);

        CreateBankCardResult CreateCard(Guid userId, Guid bankAccountId);
    }
}
