using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Applications.Tests.Features.Clients
{
    [TestFixture]
    public class ClientServiceTest
    {
        Client _client;
        Mock<IClientRepository> _mockClientRepository;
        IClientService _clientService;

        [SetUp]
        public void Initialize()
        {
            _client = new Client();
            _mockClientRepository = new Mock<IClientRepository>();
            _clientService = new ClientService(_mockClientRepository.Object);
        }

        [Test]
        public void Client_Applications_Add_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _mockClientRepository.Setup(cr => cr.Add(_client)).Returns(_client);

            //Action
            int addedClientId = _clientService.Add(_client);

            //Assert
            _mockClientRepository.Verify(cr => cr.Add(_client));
            addedClientId.Should().Be(_client.Id);
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientNullOrEmptyNameException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyName();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyNameException>();
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientNameOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientNameOverflow();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNameOverflowException>();
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientNullOrEmptyCPFException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyCPF();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyCPFException>();
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientCPFOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientCPFOverflow();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientCPFOverflowException>();
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientNullOrEmptyRGException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyRG();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyRGException>();
        }

        [Test]
        public void Client_Applications_Add_ShouldThrowClientRGOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientRGOverflow();

            //Action
            Action act = () => _clientService.Add(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientRGOverflowException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _client.Id = 1;
            _mockClientRepository.Setup(cr => cr.Update(_client)).Returns(true);
            _mockClientRepository.Setup(cr => cr.GetById(_client.Id)).Returns(_client);

            //Action
            bool updatedClient = _clientService.Update(_client);

            //Assert
            _mockClientRepository.Verify(cr => cr.Update(_client));
            updatedClient.Should().Be(true);
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientNullOrEmptyNameException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyName();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyNameException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientNameOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientNameOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNameOverflowException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientNullOrEmptyCPFException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyCPF();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyCPFException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientCPFOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientCPFOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientCPFOverflowException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientNullOrEmptyRGException()
        {
            //Arrange
            _client = ObjectMother.ClientEmptyRG();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientNullOrEmptyRGException>();
        }

        [Test]
        public void Client_Applications_Update_ShouldThrowClientRGOverflowException()
        {
            //Arrange
            _client = ObjectMother.ClientRGOverflow();
            _client.Id = 1;

            //Action
            Action act = () => _clientService.Update(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<ClientRGOverflowException>();
        }

        [Test]
        public void Client_Applications_GetById_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithId();
            var clientId = 1;
            _mockClientRepository.Setup(cr => cr.GetById(clientId)).Returns(_client);

            //Action
            Client getClient = _clientService.GetById(clientId);

            //Assert
            _mockClientRepository.Verify(cr => cr.GetById(_client.Id));
            getClient.Should().Be(_client);
        }

        [Test]
        public void Client_Applications_GetById_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _clientService.GetById(_client.Id);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Client_Applications_GetAll_ShouldBeOk()
        {
            //Arrange
            var quantity = 1;
            _client = ObjectMother.ValidClientWithoutId();
            var clientRepositoryMockValue = new List<Client>() { _client }.AsQueryable();
            _mockClientRepository.Setup(cr => cr.GetAll(quantity)).Returns(clientRepositoryMockValue);

            //Action
            var clients = _clientService.GetAll(quantity);

            //Assert
            _mockClientRepository.Verify(cr => cr.GetAll(quantity), Times.Once);
            clients.Should().NotBeNull();
            clients.Count().Should().Be(clientRepositoryMockValue.Count());
            clients.First().Should().Be(clientRepositoryMockValue.First());
        }

        [Test]
        public void Client_Applications_Remove_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _client.Id = 1;
            _mockClientRepository.Setup(cr => cr.Remove(_client.Id)).Returns(true);

            //Action
            bool clientDeleted = _clientService.Remove(_client);

            //Assert
            _mockClientRepository.Verify(cr => cr.Remove(_client.Id));
            clientDeleted.Should().BeTrue();
        }

        [Test]
        public void Client_Applications_Remove_ShouldThrowIdentifierUndefinedException()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = () => _clientService.Remove(_client);

            //Assert
            _mockClientRepository.VerifyNoOtherCalls();
            act.Should().Throw<IdentifierUndefinedException>();
        }
    }
}
