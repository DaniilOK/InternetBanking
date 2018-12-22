using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using IB.Repository.Interface;
using IB.Repository.Interface.Models;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models;
using IB.Services.Interface.Models.Enums;
using IB.Services.Mapping;

namespace IB.Services
{
    public class AccountService : AppService, IAccountService
    {
        public AccountService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory) : base(unitOfWork, validatorFactory)
        {
        }

        public BankAccountModel GetAccount(Guid id)
        {
            var bankAccount = UnitOfWork.BankAccountRepository.Find(id);
            return bankAccount?.ToBankAccountModel();
        }

        public IReadOnlyCollection<BankAccountModel> GetAccounts(Guid userId)
        {
            var bankAccounts = UnitOfWork.BankAccountRepository.GetUserBankAccounts(userId);
            return bankAccounts?.Select(x => x.ToBankAccountModel()).ToList();
        }

        public CardModel GetCard(Guid id)
        {
            var bankCard = UnitOfWork.BankCardRepository.Find(id);
            return bankCard?.ToCardModel();
        }

        public IReadOnlyCollection<CardModel> GetCards(Guid userId)
        {
            var cards = UnitOfWork.BankCardRepository.GetUserCards(userId);
            return cards?.Select(x => x.ToCardModel()).ToList();
        }

        public CreateAccountResult CreateAccount(Guid userId)
        {
            var user = UnitOfWork.UserRepository.Find(userId);

            if (user == null)
            {
                return CreateAccountResult.UserNotFound;
            }

            var bankAccount = BankAccount.CreateBankAccount(userId);
            UnitOfWork.BankAccountRepository.Add(bankAccount);

            return CreateAccountResult.Success;
        }

        public CreateBankCardResult CreateCard(Guid userId, Guid bankAccountId)
        {
            var user = UnitOfWork.UserRepository.Find(userId);
            var bankAccount = UnitOfWork.BankAccountRepository.Find(bankAccountId);

            if (user == null)
            {
                return CreateBankCardResult.UserNotFound;
            }

            if (bankAccount == null)
            {
                return CreateBankCardResult.BankAccountNotFound;
            }

            var bankCard = BankCard.CreateNewBankCard(userId, bankAccountId);
            UnitOfWork.BankCardRepository.Add(bankCard);

            return CreateBankCardResult.Success;
        }
    }
}
