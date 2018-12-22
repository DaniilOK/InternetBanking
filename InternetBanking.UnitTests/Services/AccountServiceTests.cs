using IB.Repository.Interface;
using IB.Repository.Interface.Models;
using IB.Repository.Interface.Repositories;
using IB.Services;
using IB.Services.Interface.Interfaces;
using IB.Services.Interface.Validation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using IB.Services.Interface.Models.Enums;

namespace InternetBanking.UnitTests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private IAccountService _accountService;
        private Mock<IBankAccountRepository> mockBankAccountRepository;
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IBankCardRepository> mockBankCardRepository;

        private User user = new User
        {
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            Login = "TestLogin",
            Email = "Test@test.com",
            Inactive = false,
            IsEmailConfirmed = true,
            PasswordHash = "098f6bcd4621d373cade4e832627b4f6", // md5('test')
            PasswordSalt = string.Empty
        };

        private BankAccount bankAccount = new BankAccount
        {
            Number = 1,
            EndDate = DateTime.Today,
            Active = true,
            Money = 1
        };

        private BankCard card = new BankCard
        {
            Active = true,
            Number = 1234123412341234,
            Validity = new DateTime(2019, 1, 1),
            PinCode = "1432",
            VerificationCode = "261"
        };

        [OneTimeSetUp]
        public void InitializeOnce()
        {
            user.Id = Guid.NewGuid();

            bankAccount.Id = Guid.NewGuid();
            bankAccount.UserId = user.Id;
            bankAccount.User = user;

            card.Id = Guid.NewGuid();
            card.BankAccountId = bankAccount.Id;
            card.UserId = user.Id;
            card.User = user;
            card.BankAccount = bankAccount;
        }

        [SetUp]
        public void Initialize()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockBankAccountRepository = new Mock<IBankAccountRepository>();
            mockUserRepository = new Mock<IUserRepository>();
            mockBankCardRepository = new Mock<IBankCardRepository>();
            
            mockUnitOfWork.Setup(m => m.UserRepository).Returns(mockUserRepository.Object);
            mockUnitOfWork.Setup(m => m.BankAccountRepository).Returns(mockBankAccountRepository.Object);
            mockUnitOfWork.Setup(m => m.BankCardRepository).Returns(mockBankCardRepository.Object);

            var validatorFactory = new ValidatorFactory();

            _accountService = new AccountService(mockUnitOfWork.Object, validatorFactory);
        }

        [Test]
        public void GetAccountTest()
        {
            mockBankAccountRepository.Setup(m => m.Find(It.Is<Guid>(x => x == bankAccount.Id)))
                .Returns(bankAccount);

            var obtainedBankAccountModel = _accountService.GetAccount(bankAccount.Id);

            Assert.That(obtainedBankAccountModel.Id, Is.EqualTo(bankAccount.Id));
            Assert.That(obtainedBankAccountModel.UserId, Is.EqualTo(bankAccount.UserId));
            Assert.That(obtainedBankAccountModel.Number, Is.EqualTo(bankAccount.Number));
            Assert.That(obtainedBankAccountModel.EndDate, Is.EqualTo(bankAccount.EndDate));
            Assert.That(obtainedBankAccountModel.Active, Is.EqualTo(bankAccount.Active));
            Assert.That(obtainedBankAccountModel.Money, Is.EqualTo(bankAccount.Money));
        }

        [Test]
        public void GetAccountsTest()
        {
            var userBankAccounts = new List<BankAccount> {bankAccount};
            mockBankAccountRepository.Setup(m => m.GetUserBankAccounts(It.Is<Guid>(x => x == user.Id)))
                .Returns(userBankAccounts);

            var bankAccounts = _accountService.GetAccounts(user.Id).ToList();

            Assert.AreEqual(userBankAccounts.Count, bankAccounts.Count);
            for (var i = 0; i < userBankAccounts.Count; i++)
            {
                var userBankAccount = userBankAccounts[i];
                var bankAccount = bankAccounts[i];
                Assert.That(userBankAccount.Id, Is.EqualTo(bankAccount.Id));
                Assert.That(userBankAccount.UserId, Is.EqualTo(bankAccount.UserId));
                Assert.That(userBankAccount.Number, Is.EqualTo(bankAccount.Number));
                Assert.That(userBankAccount.EndDate, Is.EqualTo(bankAccount.EndDate));
                Assert.That(userBankAccount.Active, Is.EqualTo(bankAccount.Active));
                Assert.That(userBankAccount.Money, Is.EqualTo(bankAccount.Money));
            }
        }

        [Test]
        public void GetCardTest()
        {
            mockBankCardRepository.Setup(m => m.Find(It.Is<Guid>(x => x == card.Id))).Returns(card);

            var bankCard = _accountService.GetCard(card.Id);

            Assert.That(card.Id, Is.EqualTo(bankCard.Id));
            Assert.That(card.Number, Is.EqualTo(bankCard.Number));
            Assert.That(card.BankAccountId, Is.EqualTo(bankCard.BankAccountId));
            Assert.That(card.Active, Is.EqualTo(bankCard.Active));
        }

        [Test]
        public void GetCardsTest()
        {
            var userCards = new List<BankCard> {card};
            mockBankCardRepository.Setup(m => m.GetUserCards(It.Is<Guid>(x => x == user.Id)))
                .Returns(userCards);

            var cards = _accountService.GetCards(user.Id).ToList();

            Assert.AreEqual(userCards.Count, cards.Count);
            for (var i = 0; i < cards.Count; i++)
            {
                var userCard = userCards[i];
                var card = cards[i];
                Assert.That(card.Id, Is.EqualTo(userCard.Id));
                Assert.That(card.Number, Is.EqualTo(userCard.Number));
                Assert.That(card.BankAccountId, Is.EqualTo(userCard.BankAccountId));
                Assert.That(card.Active, Is.EqualTo(userCard.Active));
            }
        }

        [Test]
        public void CreateAccount_UserNotFould()
        {
            var result = _accountService.CreateAccount(user.Id);

            Assert.AreEqual(result, CreateAccountResult.UserNotFound);
        }

        [Test]
        public void CreateAccount_Success()
        {
            var bankAccountStorage = new List<BankAccount>();
            mockUserRepository.Setup(m => m.Find(It.Is<Guid>(x => x == user.Id))).Returns(user);
            mockBankAccountRepository.Setup(m => m.Add(It.Is<BankAccount>(x => x.UserId == user.Id)))
                .Callback<BankAccount>(x => bankAccountStorage.Add(x));

            var result = _accountService.CreateAccount(user.Id);

            Assert.AreEqual(result, CreateAccountResult.Success);
            Assert.AreEqual(bankAccountStorage.Count, 1);
            Assert.AreEqual(bankAccountStorage[0].UserId, user.Id);
        }

        [Test]
        public void CreateCard_UserNotFound()
        {
            var result = _accountService.CreateCard(user.Id, bankAccount.Id);

            Assert.AreEqual(result, CreateBankCardResult.UserNotFound);
        }

        [Test]
        public void CreateCard_BankAccountNotFound()
        {
            mockUserRepository.Setup(m => m.Find(It.Is<Guid>(x => x == user.Id))).Returns(user);

            var result = _accountService.CreateCard(user.Id, bankAccount.Id);

            Assert.AreEqual(result, CreateBankCardResult.BankAccountNotFound);
        }

        [Test]
        public void CreateCard_Success()
        {
            var bankCardsStorage = new List<BankCard>();
            mockUserRepository.Setup(m => m.Find(It.Is<Guid>(x => x == user.Id))).Returns(user);
            mockBankAccountRepository.Setup(m => m.Find(It.Is<Guid>(x => x == bankAccount.Id)))
                .Returns(bankAccount);
            mockBankCardRepository.Setup(m => m.Add(It.Is<BankCard>(x =>
                    x.UserId == user.Id && x.BankAccountId == bankAccount.Id)))
                .Callback<BankCard>(x => bankCardsStorage.Add(x));

            var result = _accountService.CreateCard(user.Id, bankAccount.Id);

            Assert.AreEqual(result, CreateBankCardResult.Success);
            Assert.AreEqual(bankCardsStorage.Count, 1);
            Assert.AreEqual(bankCardsStorage[0].UserId, user.Id);
            Assert.AreEqual(bankCardsStorage[0].BankAccountId, bankAccount.Id);
        }
    }
}
