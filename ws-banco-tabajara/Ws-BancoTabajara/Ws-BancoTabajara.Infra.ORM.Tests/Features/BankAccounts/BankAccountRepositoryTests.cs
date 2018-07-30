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
        private IBankAccountRepository _repository;
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
            _context.BankAccounts.Add(_bankAccountSeed);
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
            //Arrange
            var quantity = 1;

            //Action
            var bankAccounts = _repository.GetAll(quantity).ToList();

            //Assert
            bankAccounts.Should().NotBeNull();
            bankAccounts.Should().HaveCount(_context.BankAccounts.Count());
            bankAccounts.First().Should().Be(_bankAccount);
        }

        [Test]
        public void BankAccount_Repository_GetAllWithoutQuantity_ShouldBeOk()
        {
            //Arrange
            var quantity = 0;

            //Action
            var clients = _repository.GetAll(quantity);

            //Assert
            clients.Should().NotBeNull();
            clients.First().Should().Be(_bankAccountSeed);
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
        public void BankAccount_Repository_GetById_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _bankAccount = ObjectMother.BankAccountWithClientWithoutId();

            //Action
            Action act = () => _repository.GetById(_bankAccount.Id);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
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

        [Test]
        public void BankAccount_Repository_Remove_ShouldBeOk()
        {
            //Action
            var wasRemoved = _repository.Remove(_bankAccount.Id);

            //Assert
            wasRemoved.Should().BeTrue();
            _context.BankAccounts.Where(b => b.Id == _bankAccount.Id).ToList().Should().BeNullOrEmpty();
        }

        [Test]
        public void BankAccount_Repository_Remove_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            int undefinedId = 0;

            //Action
            Action act = () => _repository.Remove(undefinedId);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void BankAccount_Repository_Remove_ShouldThrowNotFoundException()
        {
            //Arrange
            int notFoundId = 10;

            //Action
            Action act = () => _repository.Remove(notFoundId);

            //Assert
            act.Should().Throw<NotFoundException>();
        }

        [Test]
        public void BankAccount_Repository_Update_ShouldBeOk()
        {
            //Assert
            int firstId = 1;
            _bankAccount = _repository.GetById(firstId);
            _bankAccount.Number = 1;

            //Action
            var wasUpdated = _repository.Update(_bankAccount);

            //Assert
            wasUpdated.Should().BeTrue();
            _repository.GetById(_bankAccount.Id).Number.Should().Be(firstId);
        }

        [Test]
        public void BankAccount_Repository_Update_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            int undefinedId = 0;
            _bankAccount.Id = undefinedId;

            //Action
            Action act = () => _repository.Update(_bankAccount);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
