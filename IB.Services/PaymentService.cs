using FluentValidation;
using IB.Repository.Interface;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;

namespace IB.Services
{
    public class PaymentService : AppService, IPaymentService
    {
        public PaymentService(IUnitOfWork unitOfWork, IValidatorFactory validatorFactory) : base(unitOfWork, validatorFactory)
        {
        }

        public TransferResult MakeTransfer(TransferCommand command)
        {
            EnsureIsValid(command);

            var fromCard = UnitOfWork.BankCardRepository.Find(command.FromCardId);
            var toCard = UnitOfWork.BankCardRepository.Find(command.ToCardId);

            if (fromCard == null)
            {
                return TransferResult.FromNotFound;
            }

            if (toCard == null)
            {
                return TransferResult.ToNotFound;
            }

            var fromBankAccount = UnitOfWork.BankAccountRepository.Find(fromCard.BankAccountId);
            var toBankAccount = UnitOfWork.BankAccountRepository.Find(toCard.BankAccountId);

            if (fromBankAccount.Money - command.Amount < 0)
            {
                return TransferResult.NotEnoughMoney;
            }

            fromBankAccount.Money -= command.Amount;
            toBankAccount.Money += command.Amount;

            return TransferResult.Success;
        }

        public PaymentResult MakePayment(CardPaymentCommand command)
        {
            EnsureIsValid(command);

            var card = UnitOfWork.BankCardRepository.Find(command.CardId);

            if (card == null)
            {
                return PaymentResult.CardNotFound;
            }

            var bankAccount = UnitOfWork.BankAccountRepository.Find(card.BankAccountId);

            if (bankAccount.Money - command.Amount < 0)
            {
                return PaymentResult.NotEnoughMoney;
            }

            bankAccount.Money -= command.Amount;

            return PaymentResult.Success;
        }

        public PaymentResult MakePayment(BankAccountPaymentCommand command)
        {
            EnsureIsValid(command);

            var bankAccount = UnitOfWork.BankAccountRepository.Find(command.BankAccountId);

            if (bankAccount == null)
            {
                return PaymentResult.BankAccountNotFound;
            }

            if (bankAccount.Money - command.Amount < 0)
            {
                return PaymentResult.NotEnoughMoney;
            }

            bankAccount.Money -= command.Amount;

            return PaymentResult.Success;
        }
    }
}
