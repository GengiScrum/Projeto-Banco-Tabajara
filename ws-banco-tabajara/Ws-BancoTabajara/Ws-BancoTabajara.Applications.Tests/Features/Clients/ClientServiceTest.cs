using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Applications.Mapping;
using Ws_BancoTabajara.Applications.Tests.Features.Initializer;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Exceptions;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Applications.Tests.Features.Clients
{
    [TestFixture]
    public class ClientServiceTest : TestApplicationBase
    {
        Client _client;
        Mock<IClientRepository> _mockClientRepository;
        IClientService _clientService;

        [SetUp]
        public void Setup()
        {
            _client = new Client();
            _mockClientRepository = new Mock<IClientRepository>();
            _clientService = new ClientService(_mockClientRepository.Object);
        }

        [Test]
        public void Client_Applications_Add_ShouldBeOk()
        {
            //Arrange
            var addClient = ObjectMother.AddClient();
            _client = Mapper.Map<ClientRegisterCommand, Client>(addClient);
            _mockClientRepository.Setup(cr => cr.Add(It.IsAny<Client>())).Returns(_client);

            //Action
            int addedClientId = _clientService.Add(addClient);

            //Assert
            _mockClientRepository.Verify(cr => cr.Add(It.IsAny<Client>()));
            addedClientId.Should().Be(_client.Id);
        }

        [Test]
        public void Client_Applications_Update_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithoutId();
            _client.Id = 1;
            var updateClient = ObjectMother.UpdateClient();
            _mockClientRepository.Setup(cr => cr.Update(_client)).Returns(true);
            _mockClientRepository.Setup(cr => cr.GetById(_client.Id)).Returns(_client);

            //Action
            bool updatedClient = _clientService.Update(updateClient);

            //Assert
            _mockClientRepository.Verify(cr => cr.Update(_client));
            updatedClient.Should().Be(true);
        }

        [Test]
        public void Client_Applications_GetById_ShouldBeOk()
        {
            //Arrange
            _client = ObjectMother.ValidClientWithId();
            var clientId = 1;
            _mockClientRepository.Setup(cr => cr.GetById(clientId)).Returns(_client);

            //Action
            ClientViewModel getClient = _clientService.GetById(clientId);

            //Assert
            _mockClientRepository.Verify(cr => cr.GetById(_client.Id));
            getClient.CPF.Should().Be(_client.CPF);
            getClient.RG.Should().Be(_client.RG);
            getClient.Name.Should().Be(_client.Name);
        }

        [Test]
        public void Client_Applications_GetAll_ShouldBeOk()
        {
            //Arrange
            var quantity = new ClientQuery { Quantity = 1 };
            _client = ObjectMother.ValidClientWithoutId();
            var clientRepositoryMockValue = new List<Client>() { _client }.AsQueryable();
            _mockClientRepository.Setup(cr => cr.GetAll(quantity.Quantity)).Returns(clientRepositoryMockValue);

            //Action
            var clients = _clientService.GetAll(quantity);

            //Assert
            _mockClientRepository.Verify(cr => cr.GetAll(quantity.Quantity), Times.Once);
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
            var removeClient = ObjectMother.RemoveClient();

            //Action
            bool clientDeleted = _clientService.Remove(removeClient);

            //Assert
            _mockClientRepository.Verify(cr => cr.Remove(_client.Id));
            clientDeleted.Should().BeTrue();
        }
    }
}
