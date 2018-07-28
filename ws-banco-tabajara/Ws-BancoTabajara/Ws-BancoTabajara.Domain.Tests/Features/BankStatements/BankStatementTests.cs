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
using Ws_BancoTabajara.Domain.Features.BankStatements;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Domain.Tests.Features.BankStatements
{
    [TestFixture]
    public class BankStatementTests
    {
        BankStatement _bankStatement;
        Mock<BankAccount> _mockBankAccount;

        [SetUp]
        public void Initialize()
        {
            _bankStatement = new BankStatement();
            _mockBankAccount = new Mock<BankAccount>();
        }

        [Test]
        public void BankStatement_Domain_GenerateBankStatement_ShouldBeOk()
        {
            //Arrange
            string clientName = "José";
            _mockBankAccount.Setup(mb => mb.Client).Returns(new Client() { Name = clientName });

            //Action
            Action act = () => _bankStatement.GenerateBankStatement(_mockBankAccount.Object);

            //Verify
            act.Should().NotThrow();
            _bankStatement.ClientName.Should().Be(clientName);
        }
    }
}
