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
using Ws_BancoTabajara.Domain.Features.Transactions;

namespace Ws_BancoTabajara.Domain.Tests.Features.Transactions
{
    [TestFixture]
    public class TransactionTests
    {
        Transaction _transaction;
        Mock<BankAccount> _mockBankAccount;

        [SetUp]
        public void SetUp()
        {
            _transaction = new Transaction();
            _mockBankAccount = new Mock<BankAccount>();
        }

        [Test]
        public void Transaction_Domain_Validate_ShouldBeOk()
        {
            //Arrange
            _transaction = ObjectMother.ValidCreditTransaction(_mockBankAccount.Object);

            //Action
            Action act = () => _transaction.Validate();

            //Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Transaction_Domain_Validate_ShouldThrowTransactionInvalidValueException()
        {
            //Arrange
            _transaction = ObjectMother.TransactionInvalidValue(_mockBankAccount.Object);

            //Action
            Action act = () => _transaction.Validate();

            //Assert
            act.Should().Throw<TransactionInvalidValueException>();
        }

        [Test]
        public void Transaction_Domain_Validate_ShouldThrowTransactionNullBankAccount()
        {
            //Arrange
            _transaction = ObjectMother.TransactionWithoutBankAccount();

            //Action
            Action act = () => _transaction.Validate();

            //Assert
            act.Should().Throw<TransactionNullBankAccount>();
        }
    }
}
