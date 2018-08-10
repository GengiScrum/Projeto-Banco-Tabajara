using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Queries;
using Ws_BancoTabajara.Applications.Mapping;
using Ws_BancoTabajara.Applications.Tests.Features.Initializer;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Applications.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountServiceTests : TestApplicationBase
    {
        BankAccount _bankAccount;
        BankAccountRegisterCommand _bankAccountRegisterCommand;
        BankAccountUpdateCommand _bankAccountUpdateCommand;
        BankAccountRemoveCommand _bankAccountRemoveCommand;
        Mock<IBankAccountRepository> _mockBankAccountRepository;
        Mock<IClientRepository> _mockClientRepository;
        Mock<ITransactionRepository> _mockTransactionRepository;
        Mock<Client> _mockClient;
        IBankAccountService _bankAccountService;

        [SetUp]
        public void Initialize()
        {
            _bankAccountRegisterCommand = new BankAccountRegisterCommand();
            _bankAccountUpdateCommand = new BankAccountUpdateCommand();
            _bankAccountRemoveCommand = new BankAccountRemoveCommand();
            _mockBankAccountRepository = new Mock<IBankAccountRepository>();
            _mockClientRepository = new Mock<IClientRepository>();
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockClient = new Mock<Client>();
            _bankAccountService = new BankAccountService(_mockBankAccountRepository.Object, _mockTransactionRepository.Object, _mockClientRepository.Object);
        }


        [Test]
        public void BankAccount_Applications_Add_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _bankAccountRegisterCommand = ObjectMother.BankAccountRegister();
            _mockBankAccountRepository.Setup(br => br.Add(It.IsAny<BankAccount>())).Returns(_bankAccount);

            //Action
            int addedBankAccountId = _bankAccountService.Add(_bankAccountRegisterCommand);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Add(It.IsAny<BankAccount>()));
            addedBankAccountId.Should().Be(_bankAccount.Id);
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _bankAccountUpdateCommand = ObjectMother.BankAccountUpdate();
            _mockBankAccountRepository.Setup(br => br.Update(_bankAccount)).Returns(true);
            _mockBankAccountRepository.Setup(br => br.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            var updated = _bankAccountService.Update(_bankAccountUpdateCommand);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
            updated.Should().BeTrue();
        }

        [Test]
        public void BankAccount_Applications_Update_ShouldThrowBankAccountUpdateWithANewNumberException()
        {
            //Arrange
            int oldNumber = 1;
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _bankAccountUpdateCommand = ObjectMother.BankAccountUpdate();
            _mockBankAccountRepository.Setup(br => br.GetById(_bankAccount.Id)).Returns(new BankAccount() { Number = oldNumber });

            //Action
            Action act = () => _bankAccountService.Update(_bankAccountUpdateCommand);

            //Assert
            act.Should().Throw<BankAccountUpdateWithANewNumberException>();
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
            BankAccountQuery query = new BankAccountQuery() { Quantity = 1 };
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var bankAccountRepositoryGetAllMockValue = new List<BankAccount>() { _bankAccount }.AsQueryable();
            _mockBankAccountRepository.Setup(br => br.GetAll(query.Quantity)).Returns(bankAccountRepositoryGetAllMockValue);

            //Action
            var bankAccounts = _bankAccountService.GetAll(query);

            //Assert
            _mockBankAccountRepository.Verify(br => br.GetAll(query.Quantity), Times.Once);
            bankAccounts.Should().NotBeNull();
            bankAccounts.Count().Should().Be(bankAccountRepositoryGetAllMockValue.Count());
            bankAccounts.First().Should().Be(bankAccountRepositoryGetAllMockValue.First());
        }

        [Test]
        public void BankAccount_Applications_Remove_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            _bankAccountRemoveCommand = ObjectMother.BankAccountRemove();
            _mockBankAccountRepository.Setup(br => br.Remove(_bankAccount.Id)).Returns(true);

            //Action
            bool bankAccountDeleted = _bankAccountService.Remove(_bankAccountRemoveCommand);

            //Assert
            _mockBankAccountRepository.Verify(br => br.Remove(_bankAccount.Id));
            bankAccountDeleted.Should().BeTrue();
        }

        [Test]
        public void BankAccount_Applications_Withdraw_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var command = ObjectMother.BankAccountOperationCommand();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            double expectedBalance = -400;
            double expectedTotalBalance = 100;

            //Action
            var withdraw = _bankAccountService.Withdraw(command);

            //Assert
            withdraw.Should().BeTrue();
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
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
            var command = ObjectMother.BankAccountOperationCommand();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            Action act = () => _bankAccountService.Withdraw(command);

            //Assert
            _mockBankAccountRepository.VerifyNoOtherCalls();
            act.Should().Throw<BankAccountWhitdrawValueHigherThanTotalBalanceException>();
        }

        [Test]
        public void BankAccount_Applications_Deposit_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var command = ObjectMother.BankAccountOperationCommand();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            double expectedBalance = 1000;
            double expectedTotalBalance = 1500;

            //Action
            var deposit = _bankAccountService.Deposit(command);

            //Assert
            deposit.Should().BeTrue();
            _mockBankAccountRepository.Verify(br => br.Update(_bankAccount));
            _bankAccount.TotalBalance.Should().Be(expectedTotalBalance);
            _bankAccount.Balance.Should().Be(expectedBalance);
            _bankAccount.Transactions.Should().HaveCount(1);
            _bankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Credit);
        }

        [Test]
        public void BankAccount_Applications_Transfer_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var command = ObjectMother.BrankAccountTransferCommand();
            BankAccount receiverBankAccount = ObjectMother.BankAccountWithClientWithAnotherId();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.GetById(receiverBankAccount.Id)).Returns(receiverBankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            _mockBankAccountRepository.Setup(b => b.Update(receiverBankAccount)).Returns(true);
            double expectedOriginBalance = -190;
            double expectedReceiverBalance = 790;

            //Action
            var transfer = _bankAccountService.Transfer(command);

            //Assert
            transfer.Should().BeTrue();
            _bankAccount.Balance.Should().Be(expectedOriginBalance);
            receiverBankAccount.Balance.Should().Be(expectedReceiverBalance);
            _bankAccount.Transactions.Should().HaveCount(1);
            receiverBankAccount.Transactions.Should().HaveCount(1);
            _bankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Debit);
            receiverBankAccount.Transactions.First().OperationType.Should().Be(OperationTypeEnum.Credit);
        }

        [Test]
        public void BankAccount_Applications_Transfer_ShouldBeFalse()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            var command = ObjectMother.BrankAccountTransferCommand();
            BankAccount receiverBankAccount = ObjectMother.BankAccountWithClientWithAnotherId();
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);
            _mockBankAccountRepository.Setup(b => b.GetById(receiverBankAccount.Id)).Returns(receiverBankAccount);
            _mockBankAccountRepository.Setup(b => b.Update(_bankAccount)).Returns(true);
            _mockBankAccountRepository.Setup(b => b.Update(receiverBankAccount)).Returns(false);

            //Action
            var transfer = _bankAccountService.Transfer(command);

            //Assert
            transfer.Should().BeFalse();
        }

        [Test]
        public void BankAccount_Applications_GenerateBankStatement_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            _mockBankAccountRepository.Setup(b => b.GetById(_bankAccount.Id)).Returns(_bankAccount);

            //Action
            var generatedBankStatement = _bankAccountService.GenerateBankStatement(_bankAccount.Id);

            //Assert
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
