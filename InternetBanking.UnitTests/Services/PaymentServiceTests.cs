using System;
using IB.Repository.Interface;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;
using IB.Services;
using IB.Services.Interface.Commands;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Models.Enums;
using IB.Services.Interface.Validation;
using Moq;
using NUnit.Framework;

namespace InternetBanking.UnitTests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private IPaymentService _paymentService;
        private Mock<IBankAccountRepository> mockBankAccountRepository;
        private Mock<IBankCardRepository> mockBankCardRepository;


        private BankAccount bankAccount1 = new BankAccount
        {
            Number = 1,
            EndDate = DateTime.Today,
            Active = true,
            Money = 100
        };

        private BankAccount bankAccount2 = new BankAccount
        {
            Number = 2,
            EndDate = DateTime.Today,
            Active = true,
            Money = 120
        };

        private BankCard card1 = new BankCard
        {
            Active = true,
            Number = 1234123412341234,
            Validity = new DateTime(2019, 1, 1),
            PinCode = "1432",
            VerificationCode = "261"
        };

        private BankCard card2 = new BankCard
        {
            Active = true,
            Number = 1234164372341234,
            Validity = new DateTime(2019, 1, 1),
            PinCode = "8635",
            VerificationCode = "342"
        };

        [OneTimeSetUp]
        public void InitializeOnce()
        {
            bankAccount1.Id = Guid.NewGuid();
            bankAccount2.Id = Guid.NewGuid();

            card1.Id = Guid.NewGuid();
            card1.BankAccountId = bankAccount1.Id;
            card1.BankAccount = bankAccount1;

            card2.Id = Guid.NewGuid();
            card2.BankAccountId = bankAccount2.Id;
            card2.BankAccount = bankAccount2;
        }

        [SetUp]
        public void Initialize()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockBankAccountRepository = new Mock<IBankAccountRepository>();
            mockBankCardRepository = new Mock<IBankCardRepository>();

            mockUnitOfWork.Setup(m => m.BankAccountRepository).Returns(mockBankAccountRepository.Object);
            mockUnitOfWork.Setup(m => m.BankCardRepository).Returns(mockBankCardRepository.Object);

            var validatorFactory = new ValidatorFactory();

            _paymentService = new PaymentService(mockUnitOfWork.Object, validatorFactory);
        }

        [Test]
        public void MakeTransfer_FromNotFound()
        {
            var command = new TransferCommand
            {
                FromCardId = card1.Id,
                ToCardId = card2.Id,
                Amount = 10
            };

            var result = _paymentService.MakeTransfer(command);

            Assert.AreEqual(result, TransferResult.FromNotFound);
        }

        [Test]
        public void MakeTransfer_ToNotFound()
        {
            var command = new TransferCommand
            {
                FromCardId = card1.Id,
                ToCardId = card2.Id,
                Amount = 10
            };
            mockBankCardRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(card1)
                .Returns(() => null);

            var result = _paymentService.MakeTransfer(command);

            Assert.AreEqual(result, TransferResult.ToNotFound);
        }

        [Test]
        public void MakeTransfer_NotEnoughMoney()
        {
            var command = new TransferCommand
            {
                FromCardId = card1.Id,
                ToCardId = card2.Id,
                Amount = 10 + bankAccount1.Money
            };
            mockBankCardRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(card1)
                .Returns(card2);
            mockBankAccountRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(bankAccount1)
                .Returns(bankAccount2);

            var result = _paymentService.MakeTransfer(command);

            Assert.AreEqual(result, TransferResult.NotEnoughMoney);
        }

        [Test]
        public void MakeTransfer_Success()
        {
            var command = new TransferCommand
            {
                FromCardId = card1.Id,
                ToCardId = card2.Id,
                Amount = 10
            };
            var bankAccount1Money = bankAccount1.Money;
            var bankAccount2Money = bankAccount2.Money;
            mockBankCardRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(card1)
                .Returns(card2);
            mockBankAccountRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(bankAccount1)
                .Returns(bankAccount2);

            var result = _paymentService.MakeTransfer(command);

            Assert.AreEqual(result, TransferResult.Success);
            Assert.AreEqual(bankAccount1.Money, bankAccount1Money - command.Amount);
            Assert.AreEqual(bankAccount2.Money, bankAccount2Money + command.Amount);
        }

        [Test]
        public void MakePayment_CardNotFound()
        {
            var command = new CardPaymentCommand
            {
                CardId = card1.Id,
                Amount = 10
            };

            var result = _paymentService.MakePayment(command);

            Assert.AreEqual(result, PaymentResult.CardNotFound);
        }

        [Test]
        public void MakePayment_NotEnoughMoney()
        {
            var command = new CardPaymentCommand
            {
                CardId = card1.Id,
                Amount = 10 + bankAccount1.Money
            };
            mockBankCardRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(card1);
            mockBankAccountRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(bankAccount1);

            var result = _paymentService.MakePayment(command);

            Assert.AreEqual(result, PaymentResult.NotEnoughMoney);
        }

        [Test]
        public void MakePayment_Success()
        {
            var command = new CardPaymentCommand
            {
                CardId = card1.Id,
                Amount = 10
            };
            var bankAccount1Money = bankAccount1.Money;
            mockBankCardRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(card1);
            mockBankAccountRepository.SetupSequence(m => m.Find(It.IsAny<Guid>()))
                .Returns(bankAccount1);

            var result = _paymentService.MakePayment(command);

            Assert.AreEqual(result, PaymentResult.Success);
            Assert.AreEqual(bankAccount1.Money, bankAccount1Money - command.Amount);
        }
    }
}
