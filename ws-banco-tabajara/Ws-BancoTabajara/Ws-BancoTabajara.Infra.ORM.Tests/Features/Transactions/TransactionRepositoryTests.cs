using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Features.Transactions;
using Ws_BancoTabajara.Infra.ORM.Tests.Context;
using Ws_BancoTabajara.Infra.ORM.Tests.Initializer;

namespace Ws_BancoTabajara.Infra.ORM.Tests.Features.Transactions
{
    [TestFixture]
    public class TransactionRepositoryTests : EffortTestBase
    {
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
            _transactionSeed = ObjectMother.ValidCreditTransaction(ObjectMother.BankAccountWithClientWithoutId());
            _context.Transactions.Add(_transactionSeed);
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
        public void Transaction_Repository_GetById_ShouldBeOk()
        {
            //Arrange
            _transaction = _transactionSeed;
            _transaction.Id = 1;

            //Action
            var getTransaction = _repository.GetById(_transaction.Id);

            //Assert
            getTransaction.Last().Should().Be(_transaction);
        }
    }
}
