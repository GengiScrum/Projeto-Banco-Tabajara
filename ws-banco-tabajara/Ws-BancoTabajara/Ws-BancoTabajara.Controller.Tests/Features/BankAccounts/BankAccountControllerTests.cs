using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Ws_BancoTabajara.Api.Controllers.BankAccounts;
using Ws_BancoTabajara.Applications.Features.BankAccounts;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Commands;
using Ws_BancoTabajara.Applications.Features.BankAccounts.Queries;
using Ws_BancoTabajara.Applications.Features.BankAccounts.ViewModels;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Applications.Mapping;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Controller.Tests.Initializer;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.BankStatements;
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Controller.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountControllerTests : TestControllerBase
    {
        private BankAccountsController _bankAccountController;
        private Mock<IBankAccountService> _mockBankAccountService;
        private Mock<BankAccount> _mockBankAccount;
        private Mock<BankAccountRegisterCommand> _mockBankAccountRegisterCommand;
        private Mock<BankAccountUpdateCommand> _mockBankAccountUpdateCommand;
        private Mock<BankAccountRemoveCommand> _mockBankAccountRemoveCommand;
        private Mock<BankAccountTransferCommand> _mockBankAccountTransferCommand;
        private Mock<BankAccountOperationCommand> _mockBankAccountOperationCommand;
        private Mock<Client> _mockClient;
        private Mock<BankStatement> _mockBankStatement;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _mockBankAccountService = new Mock<IBankAccountService>();
            _bankAccountController = new BankAccountsController(_mockBankAccountService.Object)
            {
                Request = request
            };
            _validator = new Mock<ValidationResult>();
            _mockBankAccount = new Mock<BankAccount>();
            _mockBankAccountRegisterCommand = new Mock<BankAccountRegisterCommand>();
            _mockBankAccountRegisterCommand.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _mockBankAccountUpdateCommand = new Mock<BankAccountUpdateCommand>();
            _mockBankAccountUpdateCommand.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _mockBankAccountRemoveCommand = new Mock<BankAccountRemoveCommand>();
            _mockBankAccountRemoveCommand.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _mockBankAccountTransferCommand = new Mock<BankAccountTransferCommand>();
            _mockBankAccountTransferCommand.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _mockBankAccountOperationCommand = new Mock<BankAccountOperationCommand>();
            _mockBankAccountOperationCommand.Setup(cmd => cmd.Validate()).Returns(_validator.Object);
            _mockClient = new Mock<Client>();
            _mockBankStatement = new Mock<BankStatement>();
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }

        [Test]
        public void BankAccount_Controller_GetAll_ShouldBeOk()
        {
            //Arrange
            var bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            var response = new List<BankAccount>() { bankAccount }.AsQueryable();
            _mockBankAccountService.Setup(bas => bas.GetAll(It.IsAny<BankAccountQuery>())).Returns(response);

            //Action
            var callback = _bankAccountController.GetAll();

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<BankAccountViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(bankAccount.Id);
        }

        [Test]
        public void BankAccount_Controller_GetById_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockBankAccount.Setup(ba => ba.Id).Returns(id);
            _mockBankAccountService.Setup(bas => bas.GetById(id)).Returns(_mockBankAccount.Object);

            //Action
            IHttpActionResult callback = _bankAccountController.GetById(id);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<BankAccountViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _mockBankAccountService.Verify(bas => bas.GetById(id), Times.Once);
            _mockBankAccount.Verify(ba => ba.Id, Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Add_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockBankAccountService.Setup(bas => bas.Add(_mockBankAccountRegisterCommand.Object)).Returns(id);

            //Action
            IHttpActionResult callback = _bankAccountController.Add(_mockBankAccountRegisterCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _mockBankAccountService.Verify(bas => bas.Add(_mockBankAccountRegisterCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Update_ShouldBeOk()
        {
            //Arrange
            var isUpdated = true;
            _mockBankAccountService.Setup(bas => bas.Update(_mockBankAccountUpdateCommand.Object)).Returns(isUpdated);

            //Action
            IHttpActionResult callback = _bankAccountController.Update(_mockBankAccountUpdateCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _mockBankAccountService.Verify(bas => bas.Update(_mockBankAccountUpdateCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Remove_ShouldBeOk()
        {
            //Arrange
            var isDeleted = true;
            _mockBankAccountService.Setup(bas => bas.Remove(_mockBankAccountRemoveCommand.Object)).Returns(isDeleted);

            //Action
            IHttpActionResult callback = _bankAccountController.Remove(_mockBankAccountRemoveCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _mockBankAccountService.Verify(bas => bas.Remove(_mockBankAccountRemoveCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Withdraw_ShouldBeOk()
        {
            //Arrange
            var wasWithdrawn = true;
            _mockBankAccountService.Setup(bas => bas.Withdraw(_mockBankAccountOperationCommand.Object)).Returns(wasWithdrawn);

            //Action
            IHttpActionResult callback = _bankAccountController.Withdraw(_mockBankAccountOperationCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockBankAccountService.Verify(bas => bas.Withdraw(_mockBankAccountOperationCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Deposit_ShouldBeOk()
        {
            //Arrange
            var wasDeposited = true;
            _mockBankAccountService.Setup(bas => bas.Deposit(_mockBankAccountOperationCommand.Object)).Returns(wasDeposited);

            //Action
            IHttpActionResult callback = _bankAccountController.Deposit(_mockBankAccountOperationCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockBankAccountService.Verify(bas => bas.Deposit(_mockBankAccountOperationCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_Transfer_ShouldBeOk()
        {
            //Arrange
            var wasTransferred = true;
            _mockBankAccountService.Setup(bas => bas.Transfer(_mockBankAccountTransferCommand.Object)).Returns(wasTransferred);

            //Action
            IHttpActionResult callback = _bankAccountController.Transfer(_mockBankAccountTransferCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockBankAccountService.Verify(bas => bas.Transfer(_mockBankAccountTransferCommand.Object), Times.Once);
        }

        [Test]
        public void BankAccount_Controller_GenerateBankStatement_ShouldBeOk()
        {
            //Arrange
            var bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            _mockBankAccountService.Setup(bas => bas.GenerateBankStatement(bankAccount.Id)).Returns(_mockBankStatement.Object);

            //Action
            IHttpActionResult callback = _bankAccountController.GenerateBankStatement(bankAccount.Id);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<BankStatement>>().Subject;
            httpResponse.Content.Should().Be(_mockBankStatement.Object);
            _mockBankAccountService.Verify(bas => bas.GenerateBankStatement(bankAccount.Id));
        }

        [Test]
        public void BankAccount_Controller_ChangeActivation_ShouldBeOk()
        {
            //Arrange
            var bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            _mockBankAccountService.Setup(bs => bs.ChangeActivation(bankAccount.Id)).Returns(true);

            //Action
            IHttpActionResult callBack = _bankAccountController.ChangeActivation(bankAccount.Id);

            //Assert
            var httpResponse = callBack.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockBankAccountService.Verify(bs => bs.ChangeActivation(bankAccount.Id), Times.Once);
        }
    }
}