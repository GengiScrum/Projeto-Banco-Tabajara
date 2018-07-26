using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Domain.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Tests.Context;
using Ws_BancoTabajara.Infra.ORM.Tests.Initializer;

namespace Ws_BancoTabajara.Infra.ORM.Tests.Features.Transactions
{
    [TestFixture]
    public class TransactionRepositoryTests : EffortTestBase
    {
        private BankAccount _bankAccountSeed;
        private FakeDbContext _context;
        private ITransactionRepository _repository;
        private Transaction _transaction;
        private Transaction _transactionSeed;

        [SetUp]
        public void Setup()
        {
            _transaction = new Transaction();

            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new TransactionRepository(_context);

            //Seed
            _bankAccountSeed = ObjectMother.BankAccountWithClientWithoutId();
            _transactionSeed = ObjectMother.ValidCreditTransaction(_bankAccountSeed);
            _transactionSeed = _context.Transactions.Add(_transactionSeed);
            _context.SaveChanges();
        }

        [Test]
        public void Transaction_Repository_Add_ShouldBeOk()
        {
            //Arrange
            _transaction = ObjectMother.ValidCreditTransaction(_transactionSeed.BankAccount);

            //Action
            Transaction addedTransaction = _repository.Add(_transaction);

            //Assert
            addedTransaction.Should().Be(_transaction);
        }


        [Test]
        public void Transaction_Repository_GetManyByBankAccountId_WithQuantity_ShouldBeOk()
        {
            //Arrange
            _transaction = ObjectMother.ValidCreditTransaction(_bankAccountSeed);
            _repository.Add(_transaction);
            int quantity = 1;

            //Action
            var getTransactions = _repository.GetManyByBankAccountId(_transaction.BankAccount.Id, quantity).ToList();

            //Assert
            getTransactions.Last().Should().Be(_transaction);
        }

        [Test]
        public void Transaction_Repository_GetManyByBankAccountId_WithoutQuantity_ShouldBeOk()
        {
            //Action
            var getTransactions = _repository.GetManyByBankAccountId(_transactionSeed.BankAccount.Id).ToList();

            //Assert
            getTransactions.Should().NotBeNullOrEmpty();
            getTransactions.Should().HaveCount(1);
            getTransactions.Last().Should().Be(_transactionSeed);
        }
    }
}
