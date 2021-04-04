using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_GetbyBusiness
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_GetbyBusiness()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public async void GetbyBusiness_CallsGetbyBusinessOnMockRepo_ReturnsOkObjectResult()
        {
            //Arrange
            string authId = "Auth1234";

            mockRepo.Setup(repo => repo.GetbyBusiness(authId)).Returns(Task.FromResult<Business>(new Business {Auth0Id = authId}));
            
            //Act
            var result = await controller.GetbyBusiness(authId);
            var resultObj = result as OkObjectResult;

            //Asset
            mockRepo.Verify(repo => repo.GetbyBusiness(authId), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

        [Fact]
        public async void GetbyBusiness_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            string authId = "auth1234";

            mockRepo.Setup(repo => repo.GetbyBusiness(authId)).Throws(new Exception());

            //Act
            var result = await controller.GetbyBusiness(authId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
