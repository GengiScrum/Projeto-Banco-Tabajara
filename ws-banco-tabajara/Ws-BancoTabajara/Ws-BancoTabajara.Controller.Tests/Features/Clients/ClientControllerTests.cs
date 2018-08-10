using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Ws_BancoTabajara.Api.Controllers.Clients;
using Ws_BancoTabajara.Applications.Features.Clients;
using Ws_BancoTabajara.Applications.Features.Clients.Commands;
using Ws_BancoTabajara.Applications.Features.Clients.Queries;
using Ws_BancoTabajara.Applications.Features.Clients.ViewModels;
using Ws_BancoTabajara.Applications.Mapping;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Controller.Tests.Initializer;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Controller.Tests.Features.Clients
{
    [TestFixture]
    public class ClientControllerTests : TestControllerBase
    {
        private ClientsController _clientController;
        private Mock<IClientService> _mockClientService;
        private Mock<Client> _mockClient;
        private Mock<ClientRemoveCommand> _mockClientRemoveCommand;
        private Mock<ClientViewModel> _mockClientViewModel;
        private Mock<ClientRegisterCommand> _mockClientRegisterCommand;
        private Mock<ClientUpdateCommand> _mockClientUpdateCommand;

        [SetUp]
        public void Setup()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _mockClientService = new Mock<IClientService>();
            _mockClientRemoveCommand = new Mock<ClientRemoveCommand>();
            _mockClientViewModel = new Mock<ClientViewModel>();
            _mockClientRegisterCommand = new Mock<ClientRegisterCommand>();
            _mockClientUpdateCommand = new Mock<ClientUpdateCommand>();

            _clientController = new ClientsController(_mockClientService.Object)
            {
                Request = request
            };
            _mockClient = new Mock<Client>();
        }

        [Test]
        public void Client_Controller_GetAll_ShouldBeOk()
        {
            //Arrange
            var client = ObjectMother.ValidClientWithId();
            var response = new List<Client>() { client }.AsQueryable();
            _mockClientService.Setup(cs => cs.GetAll(It.IsAny<ClientQuery>())).Returns(response);

            //Action
            var callback = _clientController.GetAll();

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ClientViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(client.Id);
        }

        [Test]
        public void Client_Controller_GetById_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockClientViewModel.Object.Id = id;
            _mockClientService.Setup(cs => cs.GetById(id)).Returns(_mockClientViewModel.Object);

            //Action
            IHttpActionResult callback = _clientController.GetById(id);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ClientViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _mockClientService.Verify(cs => cs.GetById(id), Times.Once);
        }

        [Test]
        public void Client_Controller_Add_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockClientRegisterCommand.Object.Name = "Tal";
            _mockClientRegisterCommand.Object.CPF = "123.456.789-09";
            _mockClientRegisterCommand.Object.RG = "12345678";
            _mockClientRegisterCommand.Object.BirthDate = "09/09/2000";
            _mockClientService.Setup(cs => cs.Add(_mockClientRegisterCommand.Object)).Returns(id);

            //Action
            IHttpActionResult callback = _clientController.Add(_mockClientRegisterCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _mockClientService.Verify(cs => cs.Add(_mockClientRegisterCommand.Object), Times.Once);
        }

        [Test]
        public void Client_Controller_Update_ShouldBeOk()
        {
            //Arrange
            var isUpdated = true;
            _mockClientUpdateCommand.Object.Id = 1;
            _mockClientUpdateCommand.Object.Name = "Tal";
            _mockClientUpdateCommand.Object.CPF = "123.456.789-09";
            _mockClientUpdateCommand.Object.RG = "12345678";
            _mockClientUpdateCommand.Object.BirthDate = "09/09/2000";
            _mockClientService.Setup(cs => cs.Update(_mockClientUpdateCommand.Object)).Returns(isUpdated);

            //Action
            IHttpActionResult callback = _clientController.Update(_mockClientUpdateCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _mockClientService.Verify(cs => cs.Update(_mockClientUpdateCommand.Object), Times.Once);
        }

        [Test]
        public void Client_Controller_Remove_ShouldBeOk()
        {
            //Arrange
            var isDeleted = true;
            _mockClientRemoveCommand.Object.Id = 1;
            _mockClientService.Setup(cs => cs.Remove(_mockClientRemoveCommand.Object)).Returns(isDeleted);

            //Action
            IHttpActionResult callback = _clientController.Remove(_mockClientRemoveCommand.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockClientService.Verify(cs => cs.Remove(_mockClientRemoveCommand.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }
    }
}
