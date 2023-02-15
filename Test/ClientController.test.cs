using ApiTest.Controllers;
using ApiTest.Interfaces;
using ApiTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test
{
    public class Tests
    {
        private Mock<IClientRepository> _mockClientRepository;
        private ClientController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _controller = new ClientController(_mockClientRepository.Object);
        }

        [Test]
        public async Task Save_ValidClient_ReturnsOk()
        {
            var person = new Person { Name = "Test", Address="calle 50", Age="20", Identification="45682" };
            var client = new Client { Password = "1234", State = "True", oPerson=person};


            var result = await _controller.save(client) as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            _mockClientRepository.Verify(x => x.Create(client), Times.Once);
        }

        [Test]
        public async Task Save_InvalidClient_ReturnsInternalServerError()
        {
            var person = new Person { Name = "Test", Address = "calle 50", Age = "20", Identification = "45682" };
            var client = new Client { Password = "1234", State = "True", oPerson = person };

            _mockClientRepository.Setup(x => x.Create(client)).ThrowsAsync(new Exception(""));

          
            var result = await _controller.save(client);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            _mockClientRepository.Verify(x => x.Create(client), Times.Once);
        }
    }
}