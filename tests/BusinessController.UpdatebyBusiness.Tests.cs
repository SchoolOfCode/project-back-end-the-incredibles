using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_UpdateByBusiness
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_UpdateByBusiness()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public async void UpdatebyBusiness_CallsUpdatebyBusinessOnMockRepo_ReturnsCreatedObjectResult()
        {
            //Arrange
            long businessId = 1;
            var newBusiness = new Business() { };

            mockRepo.Setup(repo => repo.UpdatebyBusiness(newBusiness)).Returns(Task.FromResult<Business>(newBusiness));

            //Act
            var result = await controller.UpdatebyBusiness(businessId, newBusiness);
            var resultObj = result as OkObjectResult;

            //Asset
            mockRepo.Verify(repo => repo.UpdatebyBusiness(newBusiness), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

        [Fact]
        public async void UpdatebyBusiness_UpdatesValuesForBusinessWithCorrectId()
        {
            //Arrange
            long expectedId = 1;
            var newBusiness = new Business() { };

            mockRepo.Setup(repo => repo.UpdatebyBusiness(newBusiness)).Returns(Task.FromResult<Business>(newBusiness));

            //Act
            var result = await controller.UpdatebyBusiness(expectedId, newBusiness);
            var resultObj = result as OkObjectResult;
            var updatedBusiness = resultObj.Value as Business;

            //Asset
            Assert.NotNull(updatedBusiness);
            Assert.Equal(expectedId, updatedBusiness.Id);
        }

        [Fact]
        public async void UpdatebyBusiness_ReturnsResultWithBusinessFieldsUpdated()
        {
           // Arrange
            long businessId = 1;
            var expectedBusiness = new Business() { BusinessName= "updated business"};

            mockRepo.Setup(repo => repo.UpdatebyBusiness(expectedBusiness)).Returns(Task.FromResult<Business>(expectedBusiness));

            //Act
            var result = await controller.UpdatebyBusiness(businessId, expectedBusiness);
            var resultObj = result as OkObjectResult;
            var actualBusiness = resultObj.Value as Business;

            //Asset
            Assert.Equal(expectedBusiness.BusinessName, actualBusiness.BusinessName);
        }

        [Fact]
        public async void UpdatebyBusiness_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            long businessId = 4;
            var newBusiness = new Business() { };

            mockRepo.Setup(repo => repo.UpdatebyBusiness(newBusiness)).Throws(new Exception());

            //Act
            var result = await controller.UpdatebyBusiness(businessId, newBusiness);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
