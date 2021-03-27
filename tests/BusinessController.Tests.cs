using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public async void AllMethods_WhenRepoThrowsException_ReturnsBadRequest(string method)
        {
            //Is there a way to call all methods in one test??

            //Arrange

            //Act

            //Assert
            Assert.Equal(404, resultObj.StatusCode); ;

        }
        [Fact]
        public async void GetAll_ReturnsOkObjectWhenSearchIsNull()
        {
            //Arrange

            //Act
            var result = await controller.GetAll();
            var resultObj = result as OkObjectResult;

            //Assert
            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

        [Fact]
        public async void GetAll_WhenSearchIsNullAndRepoThrowsException_ReturnsNotFound()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAll()).Throws(new InvalidOperationException());

            //Act
            var result = await controller.GetAll();
            var resultObj = result as NotFoundObjectResult;

            //Assert
            Assert.Equal(404, resultObj.StatusCode); ;
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Dave")]
        public async void GetAll_CallsSearchMethodOnMockRepoWhenSearchIsNotNull_ReturnsOkObject(string search)
        {
            //Arrange
            mockRepo.Setup(repo => repo.Search(search));

            //Act
            var result = await controller.GetAll(search);
            var resultObj = result as OkObjectResult;

            //Asset
            mockRepo.Verify(repo => repo.Search(search), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

    }
}
