using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_InsertbyBusiness
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_InsertbyBusiness()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public async void InsertbyBusiness_CallsInsertbyBusinessOnMockRepo_ReturnsCreatedObjectResult()
        {
            //Arrange
            var expectedBusiness = new Business() { };

            mockRepo.Setup(repo => repo.InsertbyBusiness(expectedBusiness)).Returns(Task.FromResult<Business>(new Business {}));
            
            //Act
            var result = await controller.InsertbyBusiness(expectedBusiness);
            var resultObj = result as CreatedResult;

            //Asset
            mockRepo.Verify(repo => repo.InsertbyBusiness(expectedBusiness), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(201, resultObj.StatusCode);
        }

        [Fact]
        public async void InsertbyBusiness_OverridesNullNewBuisnessFromMockRepo_ReturnsOverriddenBusiness()
        {
            //Arrange
            string expectedBusinessName = "Please enter your business name";
            var newBusiness = new Business() { };

            mockRepo.Setup(repo => repo.InsertbyBusiness(newBusiness)).Returns(Task.FromResult<Business>(new Business { }));

            //Act
            var result = await controller.InsertbyBusiness(newBusiness);
            var resultObj = result as CreatedResult;
            var model = resultObj.Value as Business;

            //Asset
            Assert.NotNull(model.BusinessName);
            Assert.Equal(expectedBusinessName, model.BusinessName);
        }

        [Fact]
        public async void InsertbyBusiness_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var newBusiness = new Business() { };

            mockRepo.Setup(repo => repo.InsertbyBusiness(newBusiness)).Throws(new Exception());

            //Act
            var result = await controller.InsertbyBusiness(newBusiness);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
