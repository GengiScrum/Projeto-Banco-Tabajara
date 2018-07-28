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
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Controller.Tests.Features.Clients
{
    [TestFixture]
    public class ClientControllerTests
    {
        private ClientsController _clientController;
        private Mock<IClientService> _mockClientService;
        private Mock<Client> _mockClient;

        [SetUp]
        public void Initializer()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _mockClientService = new Mock<IClientService>();
            _clientController = new ClientsController()
            {
                Request = request,
                _clientService = _mockClientService.Object,
            };
            _mockClient = new Mock<Client>();
        }

        [Test]
        public void Client_Controller_GetAll_ShouldBeOk()
        {
            //Arrange
            var quantity = 0;
            var client = ObjectMother.ValidClientWithId();
            var response = new List<Client>() { client }.AsQueryable();
            _mockClientService.Setup(cs => cs.GetAll(quantity)).Returns(response);

            //Action
            var callback = _clientController.GetAll();

            //Assert
            _mockClientService.Verify(cs => cs.GetAll(quantity), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<Client>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(client.Id);
        }

        [Test]
        public void Client_Controller_GetById_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockClient.Setup(c => c.Id).Returns(id);
            _mockClientService.Setup(cs => cs.GetById(id)).Returns(_mockClient.Object);

            //Action
            IHttpActionResult callback = _clientController.GetById(id);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<Client>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _mockClientService.Verify(cs => cs.GetById(id), Times.Once);
            _mockClient.Verify(c => c.Id, Times.Once);
        }

        [Test]
        public void Client_Controller_Add_ShouldBeOk()
        {
            //Arrange
            var id = 1;
            _mockClientService.Setup(cs => cs.Add(_mockClient.Object)).Returns(id);

            //Action
            IHttpActionResult callback = _clientController.Add(_mockClient.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _mockClientService.Verify(cs => cs.Add(_mockClient.Object), Times.Once);
        }

        [Test]
        public void Client_Controller_Update_ShouldBeOk()
        {
            //Arrange
            var isUpdated = true;
            _mockClientService.Setup(cs => cs.Update(_mockClient.Object)).Returns(isUpdated);

            //Action
            IHttpActionResult callback = _clientController.Update(_mockClient.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _mockClientService.Verify(cs => cs.Update(_mockClient.Object), Times.Once);
        }

        [Test]
        public void Client_Controller_Remove_ShouldBeOk()
        {
            //Arrange
            var isDeleted = true;
            _mockClientService.Setup(cs => cs.Remove(_mockClient.Object)).Returns(isDeleted);

            //Action
            IHttpActionResult callback = _clientController.Remove(_mockClient.Object);

            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _mockClientService.Verify(cs => cs.Remove(_mockClient.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }
    }
}
