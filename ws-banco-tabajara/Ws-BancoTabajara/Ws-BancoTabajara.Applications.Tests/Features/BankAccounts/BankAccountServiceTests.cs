using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Applications.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountServiceTests
    {
        BankAccount _bankAccount;
        Mock<IBankAccountRepository> _mockBankAccountRepository;
        Mock<ITransactionRepository> _mockTransactionRepository;
        Mock<IClientRepository> _mockClientRepository;
        Mock<Client> _mockClient;
        IBankAccountService _bankAccountService;

        [SetUp]
        public void Initialize()
        {
            _bankAccount = new BankAccount();
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockClient = new Mock<Client>();
            _bankAccountService = new BankAccountService(_mockBankAccountRepository.Object, _mockTransactionRepository.Object, _mockClientRepository.Object);
        }


        [Test]
        public void BankAccount_Applications_Add_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(br => br.Add(_bankAccount)).Returns(_bankAccount);
            _mockClientRepository.Setup(c => c.GetById(_bankAccount.Client.Id)).Returns(_bankAccount.Client);

            //Action
            int addedBankAccountId = _bankAccountService.Add(_bankAccount);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Add(_bankAccount));
            addedBankAccountId.Should().Be(_bankAccount.Id);
        }

        [Test]
        public void BankAccount_Applications_Add_ShouldThrowBankAccountWithoutClientException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithoutId();
            _mockClientRepository.Setup(c => c.GetById(_bankAccount.Client.Id));

            //Action
            Action act = () => _bankAccountService.Add(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountWithoutClientException>();
        }

        [Test]
        public void BankAccount_Applications_Add_ShouldThrowBankAccountInvalidNumberException()
        {
            //Arrange
            _bankAccount = ObjectMother.InvalidBankAccountNumberWithoutId(_mockClient.Object);

            //Action
            Action act = () => _bankAccountService.Add(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountInvalidNumberException>();
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(br => br.Update(_bankAccount)).Returns(true);
            _mockBankAccountRepository.Setup(br => br.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            var updated = _bankAccountService.Update(_bankAccount);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
            updated.Should().BeTrue();
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithoutClientWithoutId();

            //Action
            Action act = () => _bankAccountService.Update(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldThrowBankAccountWithoutClientException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithoutClientWithoutId();
            _bankAccount.Id = 1;

            //Action
            Action act = () => _bankAccountService.Update(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountWithoutClientException>();
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldThrowBankAccountInvalidNumberException()
        {
            //Arrange
            _bankAccount = ObjectMother.InvalidBankAccountNumberWithoutId(_mockClient.Object);
            _bankAccount.Id = 1;

            //Action
            Action act = () => _bankAccountService.Update(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountInvalidNumberException>();
        }

        [Test]
        public void BankAccount_Applications_GetById_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(br => br.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            BankAccount getBankAccount = _bankAccountService.GetById(_bankAccount.Id);

            //Assert
            _mockBankAccountRepository.Verify(br => br.GetById(_bankAccount.Id));
            getBankAccount.Should().Be(_bankAccount);
        }

        [Test]
        public void BankAccount_Applications_GetById_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            int invalidId = 0;

            //Action
            Action act = () => _bankAccountService.GetById(invalidId);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BankAccount_Applications_GetAll_ShouldBeOk()
        {
            //Arrange
            var quantity = 1;
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var bankAccountRepositoryGetAllMockValue = new List<BankAccount>() { _bankAccount }.AsQueryable();
            _mockBankAccountRepository.Setup(br => br.GetAll(quantity)).Returns(bankAccountRepositoryGetAllMockValue);

            //Action
            var bankAccounts = _bankAccountService.GetAll(quantity);

            //Assert
            _mockBankAccountRepository.Verify(br => br.GetAll(quantity), Times.Once);
            bankAccounts.Should().NotBeNull();
            bankAccounts.Count().Should().Be(bankAccountRepositoryGetAllMockValue.Count());
            bankAccounts.First().Should().Be(bankAccountRepositoryGetAllMockValue.First());
        }

        [Test]
        public void BankAccount_Applications_Remove_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(br => br.Remove(_bankAccount.Id)).Returns(true);

            //Action
            bool bankAccountDeleted = _bankAccountService.Remove(_bankAccount);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Remove(_bankAccount.Id));
            bankAccountDeleted.Should().BeTrue();
        }

        [Test]
        public void BankAccount_Applications_Remove_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithoutId();

            //Action
            Action act = () => _bankAccountService.Remove(_bankAccount);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BankAccount_Applications_Withdraw_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            _mockTransactionRepository.Setup(tr => tr.Add(It.IsAny<Transaction>())).Returns(new Transaction { Id = 1, OperationType = OperationTypeEnum.Debit });
            double value = 700;
            double expectedBalance = -400;
            double expectedTotalBalance = 100;

            //Action
            var withdraw = _bankAccountService.Withdraw(_bankAccount.Id, value);

            //Assert
            withdraw.Should().BeTrue();
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
            _mockTransactionRepository.Verify(tr => tr.Add(It.IsAny<Transaction>()));
            _bankAccount.TotalBalance.Should().Be(expectedTotalBalance);
            _bankAccount.Balance.Should().Be(expectedBalance);
            _bankAccount.Transactions.Should().HaveCount(1);
            _bankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Debit);
        }
        
        [Test]
        public void BankAccount_Applications_Withdraw_ShouldThrowBankAccountWhitdrawValueHigherThanTotalBalanceException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            double value = 900;

            //Action
            Action act = () => _bankAccountService.Withdraw(_bankAccount.Id, value);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountWhitdrawValueHigherThanTotalBalanceException>();
        }

        [Test]
        public void BankAccount_Applications_Withdraw_ShouldThrowBankAccountInvalidTransactionValueException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithoutId();
            double value = -900;

            //Action
            Action act = () => _bankAccountService.Withdraw(_bankAccount.Id, value);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountInvalidTransactionValueException>();
        }

        [Test]
        public void BankAccount_Applications_Deposit_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            _mockTransactionRepository.Setup(tr => tr.Add(It.IsAny<Transaction>())).Returns(new Transaction { Id = 1, OperationType = OperationTypeEnum.Credit });
            double value = 100;
            double expectedBalance = 400;
            double expectedTotalBalance = 900;

            //Action
            var deposit = _bankAccountService.Deposit(_bankAccount.Id, value);

            //Assert
            deposit.Should().BeTrue();
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
            _mockTransactionRepository.Verify(tr => tr.Add(It.IsAny<Transaction>()));
            _bankAccount.TotalBalance.Should().Be(expectedTotalBalance);
            _bankAccount.Balance.Should().Be(expectedBalance);
            _bankAccount.Transactions.Should().HaveCount(1);
            _bankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Credit);
        }
        
        [Test]
        public void BankAccount_Applications_Deposit_ShouldThrowBankAccountInvalidTransactionValueException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithoutId();
            double value = -900;

            //Action
            Action act = () => _bankAccountService.Withdraw(_bankAccount.Id, value);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountInvalidTransactionValueException>();
        }

        [Test]
        public void BankAccount_Applications_Transfer_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            BankAccount receiverBankAccount = ObjectMother.BankAccountWithClientWithAnotherId();
            _mockTransactionRepository.SetupSequence(tr => tr.Add(It.IsAny<Transaction>()))
                .Returns(new Transaction() {Id = 1, OperationType = OperationTypeEnum.Debit })
                .Returns(new Transaction() {Id = 2, OperationType = OperationTypeEnum.Credit });
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.GetById(receiverBankAccount.Id)).Returns(receiverBankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            _mockBankAccountRepository.Setup(b => b.Update(receiverBankAccount)).Returns(true);
            double value = 490;
            double expectedOriginBalance = -190;
            double expectedReceiverBalance = 790;

            //Action
            var transfer = _bankAccountService.Transfer(_bankAccount.Id, receiverBankAccount.Id, value);

            //Assert
            transfer.Should().BeTrue();
            _mockTransactionRepository.Verify(tr => tr.Add(It.IsAny<Transaction>()));
            _bankAccount.Balance.Should().Be(expectedOriginBalance);
            receiverBankAccount.Balance.Should().Be(expectedReceiverBalance);
            _bankAccount.Transactions.Should().HaveCount(1);
            receiverBankAccount.Transactions.Should().HaveCount(1);
            _bankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Debit);
            receiverBankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Credit);
        }

        [Test]
        public void BankAccount_Applications_Transfer_ShouldThrowBankAccountInvalidTransactionValueException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            BankAccount receiverBankAccount = ObjectMother.BankAccountWithClientWithId();
            double value = 0;

            //Action
            Action act = () => _bankAccountService.Transfer(_bankAccount.Id, receiverBankAccount.Id, value);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountInvalidTransactionValueException>();
        }

        [Test]
        public void BankAccount_Applications_GenerateBankStatement_ShouldBeOk()
        {
            //Arrange
            _mockTransactionRepository.Setup(t => t.GetManyByBankAccountId(It.IsAny<int>())).Returns(new List<Transaction>() { new Transaction() }.AsQueryable());
            _bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            var generatedBankStatement = _bankAccountService.GenerateBankStatement(_bankAccount.Id);

            //Assert
            _mockTransactionRepository.Verify(t => t.GetManyByBankAccountId(It.IsAny<int>()), Times.Once);
            _mockBankAccountRepository.Verify(b => b.GetById(_bankAccount.Id), Times.Once);
            generatedBankStatement.Should().NotBeNull();
        }

        [Test]
        public void BankAccount_Applications_GenerateBankStatement_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            int invalidId = 0;
            _bankAccount.Id = invalidId;

            //Action
            Action act = () => _bankAccountService.GenerateBankStatement(_bankAccount.Id);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
