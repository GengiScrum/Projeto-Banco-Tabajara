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
        public void Client_Repository_Add_ShouldThrowClientNullOrEmptyNameException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyName();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyNameException>();
        }

        [Test]
        public void Client_Repository_Add_ShouldThrowClientNameOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientNameOverflow();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientNameOverflowException>();
        }

        [Test]
        public void Client_Repository_Add_ShouldThrowClientNullOrEmptyCPFException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyCPF();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyCPFException>();
        }

        [Test]
        public void Client_Repository_Add_ShouldThrowClientCPFOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientCPFOverflow();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientCPFOverflowException>();
        }

        [Test]
        public void Client_Repository_Add_ShouldThrowClientNullOrEmptyRGException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyRG();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyRGException>();
        }

        [Test]
        public void Client_Repository_Add_ShouldThrowClientRGOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientRGOverflow();

            //Action
            Action act = () => _repository.Add(_client);

            //Assert
            act.Should().Throw<ClientRGOverflowException>();
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
        public void Client_Repository_Update_ShouldThrowClientNullOrEmptyNameException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyName();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyNameException>();
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowClientNameOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientNameOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientNameOverflowException>();
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowClientNullOrEmptyCPFException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyCPF();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyCPFException>();
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowClientCPFOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientCPFOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientCPFOverflowException>();
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowClientNullOrEmptyRGException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyRG();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientNullOrEmptyRGException>();
        }

        [Test]
        public void Client_Repository_Update_ShouldThrowClientRGOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientRGOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _repository.Update(_client);

            //Assert
            act.Should().Throw<ClientRGOverflowException>();
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
            //Action
            var clients = _repository.GetAll();

            //Assert
            clients.Should().NotBeNull();
            clients.Should().HaveCount(1);
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
