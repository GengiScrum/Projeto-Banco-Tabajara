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
using Ws_BancoTabajara.Domain.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Features.Clients;
using Ws_BancoTabajara.Infra.ORM.Tests.Context;
using Ws_BancoTabajara.Infra.ORM.Tests.Initializer;

namespace Ws_BancoTabajara.Infra.ORM.Tests.Features.Clients
{
    [TestFixture]
    public class ClientRepositoryTests : EffortTestBase
    {
        private FakeDbContext _context;
        private IClientRepository _repository;
        private Client _client;
        private Client _clientSeed;

        [SetUp]
        public void Setup()
        {
            _client = new Client();

            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new ClientRepository(_context);

            //Seed
            _clientSeed = ObjectMother.ValidClientWithoutId();
            _context.Clients.Add(_clientSeed);
            _context.SaveChanges();
        }

        [Test]
        public void Client_Repository_Add_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Client addedClient = _repository.Add(_client);

            //Assert
            addedClient.Should().Be(_client);
        }

        [Test]
        public void Client_Repository_Update_ShouldBeOk()
        {
            //Arrange
            int id = 1;
            _client = _repository.GetById(id);
            _client.Name = "Neide";

            //Action
            bool updatedClient = _repository.Update(_client);

            //Assert
            updatedClient.Should().Be(true);
            _repository.GetById(_client.Id).Should().Be(_client);
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Client_Repository_GetById_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _client.Id = 1;

            //Action
            Client getClient = _repository.GetById(_client.Id);

            //Assert
            getClient.Should().Be(_client);
        }

        [Test]
        public void Client_Repository_GetById_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _repository.GetById(_client.Id);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Client_Repository_GetAll_ShouldBeOk()
        {
            //Arrange
            var quantity = 1;

            //Action
            var clients = _repository.GetAll(quantity);

            //Assert
            clients.Should().NotBeNull();
            clients.Should().HaveCount(1);
            clients.First().Should().Be(_clientSeed);
        }

        [Test]
        public void Client_Repository_GetAllWithoutQuantity_ShouldBeOk()
        {
            //Arrange
            var quantity = 0;

            //Action
            var clients = _repository.GetAll(quantity);

            //Assert
            clients.Should().NotBeNull();
            clients.First().Should().Be(_clientSeed);
        }

        [Test]
        public void Client_Repository_Remove_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _client.Id = 1;

            //Action
            bool clientDeleted = _repository.Remove(_client.Id);

            //Assert
            clientDeleted.Should().BeTrue();
        }

        [Test]
        public void Client_Repository_Remove_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _repository.Remove(_client.Id);

            //Assert
            act.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
