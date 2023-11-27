using DiffAPI.Controllers;
using DiffAPI.Models;
using DiffAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DiffAPI.Tests
{
    public class DiffUnitTests
    {
        private readonly Mock<IDiffService> _mockDiffService;
        private readonly Mock<ILogger<DiffController>> _mockLogger;
        private readonly DiffController _controller;

        public DiffUnitTests()
        {
            _mockDiffService = new Mock<IDiffService>();
            _mockLogger = new Mock<ILogger<DiffController>>();
            _controller = new DiffController(_mockDiffService.Object, _mockLogger.Object);
        }

        [Fact]
        public void UploadLeftData_ShouldCallDiffServiceAndReturnCreatedAtActionResult()
        {
            // Arrange
            var id = "testId";
            var data = new DataRequestModel { Data = "testData" };

            // Act
            var result = _controller.UploadLeftData(id, data);

            // Assert
            _mockDiffService.Verify(service => service.UploadLeftData(id, data.Data), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void UploadRightData_ShouldCallDiffServiceAndReturnCreatedAtActionResult()
        {
            // Arrange
            var id = "testId";
            var data = new DataRequestModel { Data = "testData" };

            // Act
            var result = _controller.UploadRightData(id, data);

            // Assert
            _mockDiffService.Verify(service => service.UploadRightData(id, data.Data), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void GetDiffResult_ShouldReturnNotFoundResultWhenIdDoesNotExist()
        {
            // Arrange
            var id = "testId";
            _mockDiffService.Setup(service => service.GetDiffResult(id)).Returns((DiffResultModel)(null));

            // Act
            var result = _controller.GetDiffResult(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetDiffResult_ShouldReturnOkObjectResultWithCorrectDataWhenIdExists()
        {
            // Arrange
            var id = "testId";
            var diffResult = new DiffResultModel();
            _mockDiffService.Setup(service => service.GetDiffResult(id)).Returns(diffResult);

            // Act
            var result = _controller.GetDiffResult(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(diffResult, okResult.Value);
        }
    }
}
