using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_DeletebyProduct
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_DeletebyProduct()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public void DeletebyProduct_CallsDeleteMethodbyProductOnMockRepoWithCorrectId_ReturnsOkObject()
        {
            //Arrange
            long expectedId = 12;

            mockRepo.Setup(repo => repo.DeletebyProduct(expectedId));

            //Act
            var result = controller.DeletebyProduct(expectedId);
            var resultObj = result as OkObjectResult;
            var model = resultObj.Value as String;

            //Assert
            mockRepo.Verify(repo => repo.DeletebyProduct(expectedId), Times.Once);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, resultObj.StatusCode);

            Assert.Equal(model, $"Product at {expectedId} is deleted");
        }

        [Fact]
        public void DeletebyProduct_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            long id = 5;
            mockRepo.Setup(repo => repo.DeletebyProduct(id)).Throws(new Exception());

            //Act
            var result = controller.DeletebyProduct(id);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
