using Dnka.TryDataverse.Core.Contract;
using Dnka.TryDataverse.CreateTimeEntry.Model;
using Dnka.TryDataverse.CreateTimeEntry.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Dnka.TryDataverse.CreateTimeEntry.Tests
{
    public class TimeEntryFunctionTests
    {
        private readonly Mock<ITimeEntryService> _timeEntryService = new Mock<ITimeEntryService>();
        private readonly Mock<ITimeEntryBuilder> _timeEntryBuilder = new Mock<ITimeEntryBuilder>();
        private readonly Mock<IRequestParser> _requestParser = new Mock<IRequestParser>();
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();

        private readonly TimeEntryFunction _function;

        public TimeEntryFunctionTests()
        {
            _function = new TimeEntryFunction(
              _timeEntryService.Object,
              _timeEntryBuilder.Object,
              _requestParser.Object
            );
        }

        [Fact]
        public async Task Run_WhenNoErrorRequest_ShouldReturn201()
        {
            // Act
            var responseRaw = await _function.Run(null, _logger.Object);

            //Assert
            var res = (ObjectResult)responseRaw;
            Assert.Equal(StatusCodes.Status201Created, res.StatusCode);
        }

        [Fact]
        public async Task Run_WhenNoErrorRequest_ShouldCallBuilderService()
        {
            //Arrange
            CreateTimeEntryRequest createTimeEntryRequest = new CreateTimeEntryRequest();
            _requestParser
                .Setup(x => x.Parse(It.IsAny<HttpRequest>()))
                .ReturnsAsync(createTimeEntryRequest);

            // Act
            var responseRaw = await _function.Run(null, _logger.Object);

            //Assert
            _timeEntryBuilder.Verify(x => x.Build(It.Is<CreateTimeEntryRequest>(req => req == createTimeEntryRequest)), Times.Once);
        }

        [Fact]
        public async Task Run_WhenExceptionThrown_ShouldReturnBadRequest()
        {
            //Arrange
            _requestParser
                .Setup(x => x.Parse(It.IsAny<HttpRequest>()))
                .Throws<Exception>();

            // Act
            var responseRaw = await _function.Run(null, _logger.Object);

            //Assert
            var res = (BadRequestObjectResult)responseRaw;
            Assert.Equal(StatusCodes.Status400BadRequest, res.StatusCode);
        }
    }
}
