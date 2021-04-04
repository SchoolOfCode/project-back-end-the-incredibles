using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [Theory]
        [InlineData("Auth1234")]
        [InlineData("")]
        public async void GetByBusiness_ReturnsOkObjectResultWithCorrectBusiness(string authId)
        {
            //Arrange
            string expectedId = authId;
            mockRepo.Setup(repo => repo.GetbyBusiness(authId)).Returns(Task.FromResult<Business>(new Business { Auth0Id = authId }));
            
            //Act
            var result = await controller.GetbyBusiness(authId);
            var resultObj = result as OkObjectResult;
            var model = resultObj.Value as Business;

            //Asset
            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);

            Assert.Equal(expectedId, model.Auth0Id);
        }

        public async void GetByBusiness_CallsGetProductsOnMockRepo_ReturnsOkObjectResultWithCorrectProductList()
        {
            //Arrange
            string authId = "Auth1234";
            int expectedId = 4;

            mockRepo.Setup(repo => repo.GetbyBusiness(authId)).Returns(Task.FromResult<Business>(new Business { Id = expectedId }));
            mockRepo.Setup(repo => repo.GetProducts(expectedId)).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>() { new Product { BusinessId = expectedId } }));

            //Act
            var result = await controller.GetbyBusiness(authId);
            var resultObj = result as OkObjectResult;
            var modelProducts = (resultObj.Value as Business).Products;

            //Asset
            mockRepo.Verify(repo => repo.GetProducts(expectedId), Times.Once);

            Assert.Equal(200, resultObj.StatusCode);
            Assert.NotNull(modelProducts);

            Assert.Equal(expectedId, modelProducts.First().BusinessId);
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
