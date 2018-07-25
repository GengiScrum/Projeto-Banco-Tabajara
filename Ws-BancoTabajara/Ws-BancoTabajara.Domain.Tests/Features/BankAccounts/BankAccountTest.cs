using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Domain.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountTest
    {
        BankAccount _bankAccount;
        Mock<Client> _mockClient;

        [SetUp]
        public void Initializer()
        {
            _bankAccount = new BankAccount();
            _mockClient = new Mock<Client>();
        }

        [Test]
        public void BankAccount_Domain_Validate_ShouldBeOk()
        {
            //Arrange
            _bankAccount = ObjectMother.ValidActivatedBankAccountWithoutId(_mockClient.Object);

            //Action
            Action act = () => _bankAccount.Validate();

            //Assert
            act.Should().NotThrow();
        }

        [Test]
        public void BankAccount_Domain_Validate_ShouldThrowBankAccountInvalidNumberException()
        {
            //Arrange
            _bankAccount = ObjectMother.InvalidBankAccountNumberWithoutId(_mockClient.Object);

            //Action
            Action act = () => _bankAccount.Validate();

            //Assert
            act.Should().Throw<BankAccountInvalidNumberException>();
        }

        [Test]
        public void BankAccount_Domain_Validate_ShouldThrowBankAccountWithoutClientException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithoutClientWithoutId();

            //Action
            Action act = () => _bankAccount.Validate();

            //Assert
            act.Should().Throw<BankAccountWithoutClientException>();
        }

        [Test]
        public void BankAccount_Domain_Activate_ShouldBeTrue()
        {
            //Arrange
            _bankAccount = ObjectMother.ValidDeactivatedBankAccountWithoutId(_mockClient.Object);

            //Action
            _bankAccount.Activate();

            //Assert
            _bankAccount.Activated.Should().BeTrue();
        }

        [Test]
        public void BankAccount_Domain_Activate_ShouldBeFalse()
        {
            //Arrange
            _bankAccount = ObjectMother.ValidActivatedBankAccountWithoutId(_mockClient.Object);

            //Action
            _bankAccount.Deactivate();

            //Assert
            _bankAccount.Activated.Should().BeFalse();
        }
    }
}
