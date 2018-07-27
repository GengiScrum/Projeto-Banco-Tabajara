using FluentAssertions;
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
using Ws_BancoTabajara.Api.Controllers.Features.BankAccounts;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Controller.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountControllerTests
    {
        private BankAccountController _bankAccountController;
        private Mock<IBankAccountService> _mockBankAccountService;
        private Mock<BankAccount> _mockBankAccount;
        private Mock<Client> _mockClient;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _mockBankAccountService = new Mock<IBankAccountService>();
            _bankAccountController = new BankAccountController()
            {
                Request = request,
                _bankAccountsService = _mockBankAccountService.Object,
            };
            _mockBankAccount = new Mock<BankAccount>();
            _mockClient = new Mock<Client>();
        }

        [Test]
        public void BankAccount_Controller_GetAll_ShouldBeOk()
        {
            //Arrange
            var bankAccount = ObjectMother.BankAccountWithClientWithId(_mockClient.Object);
            var response = new List<BankAccount>() { bankAccount }.AsQueryable();
            _mockBankAccountService.Setup(bas => bas.GetAll()).Returns(response);

            //Action
            var callback = _bankAccountController.GetAll();

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<BankAccount>>>().Subject;
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
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<BankAccount>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _mockBankAccountService.Verify(bas => bas.GetById(id), Times.Once);
            _mockBankAccount.Verify(ba => ba.Id, Times.Once);
        }
    }
}