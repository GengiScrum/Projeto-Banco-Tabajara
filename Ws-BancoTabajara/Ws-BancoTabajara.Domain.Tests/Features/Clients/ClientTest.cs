using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ws_BancoTabajara.Common.Tests.Base;
using Ws_BancoTabajara.Domain.Features.Clients;

namespace Ws_BancoTabajara.Domain.Tests.Features.Clients
{
    [TestFixture]
    public class ClientTest
    {
        private Client _Client;

        [SetUp]
        public void Initialize()
        {
            _Client = new Client();
        }

        [Test]
        public void Client_Domain_Validate_ShouldBeOk()
        {
            //Arrange
            _Client = ObjectMother.ValidClientWithoutId();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientNullOrEmptyNameException()
        {
            //Arrange
            _Client = ObjectMother.ClientEmptyName();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientNullOrEmptyNameException>();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientNameOverflowException()
        {
            //Arrange
            _Client = ObjectMother.ClientNameOverflow();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientNameOverflowException>();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientNullOrEmptyCPFException()
        {
            //Arrange
            _Client = ObjectMother.ClientEmptyCPF();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientNullOrEmptyCPFException>();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientCPFOverflowException()
        {
            //Arrange
            _Client = ObjectMother.ClientCPFOverflow();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientCPFOverflowException>();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientNullOrEmptyRGException()
        {
            //Arrange
            _Client = ObjectMother.ClientEmptyRG();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientNullOrEmptyRGException>();
        }

        [Test]
        public void Client_Domain_Validate_ShouldThrowClientRGOverflowException()
        {
            //Arrange
            _Client = ObjectMother.ClientRGOverflow();

            //Action
            Action act = _Client.Validate;

            //Assert
            act.Should().Throw<ClientRGOverflowException>();
        }
    }
}
