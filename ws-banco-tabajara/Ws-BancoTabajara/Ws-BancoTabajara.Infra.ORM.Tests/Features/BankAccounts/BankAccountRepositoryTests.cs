using Effort;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Features.BankAccounts;
using Ws_BancoTabajara.Infra.ORM.Tests.Context;
using Ws_BancoTabajara.Infra.ORM.Tests.Initializer;

namespace Ws_BancoTabajara.Infra.ORM.Tests.Features.BankAccounts
{
    [TestFixture]
    public class BankAccountRepositoryTests : EffortTestBase
    {
        private FakeDbContext _context;
        private BankAccountRepository _repository;
        private BankAccount _bankAccount;
        private BankAccount _bankAccountSeed;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new BankAccountRepository(_context);
            _bankAccount = ObjectMother.BankAccountWithClientWithId();
            //Seed
            _bankAccountSeed = ObjectMother.BankAccountWithClientWithoutId();
            _context.BankAccounts.Add(_bankAccount);
            _context.SaveChanges();
        }

        [Test]
        public void BankAccount_Repository_Add_ShouldBeOk()
        {
            //Action
            var bankAccountRegistered = _repository.Add(_bankAccount);

            //Assert
            bankAccountRegistered.Should().NotBeNull();
            bankAccountRegistered.Should().Be(_bankAccount);
        }

        [Test]
        public void BankAccount_Repository_GetAll_ShouldBeOk()
        {
            //Action
            var bankAccounts = _repository.GetAll().ToList();

            //Assert
            bankAccounts.Should().NotBeNull();
            bankAccounts.Should().HaveCount(_context.BankAccounts.Count());
            bankAccounts.First().Should().Be(_bankAccount);
        }

        [Test]
        public void BankAccount_Repository_GetById_ShouldBeOk()
        {
            //Action
            var bankAccountResult = _repository.GetById(_bankAccount.Id);

            //Assert
            bankAccountResult.Should().NotBeNull();
            bankAccountResult.Should().Be(_bankAccount);
        }

        [Test]
        public void BankAccount_Repository_GetById_ShouldThrowNotFoundException()
        {
            //Arrange
            int notFoundId = 10;

            //Action
            Action act = () => _repository.GetById(notFoundId);

            //Assert
            act.Should().Throw<NotFoundException>();
        }
    }
}
