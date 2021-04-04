using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    public class BusinessControllerTests_GetbyBusinessName
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_GetbyBusinessName()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }
        [Fact]
        public async void GetbyBusinessName_CallsGetbyBusinessOnMockRepo_ReturnsOkObjectResult()
        {
            //Arrange
            string expectedName = "Test";

            mockRepo.Setup(repo => repo.GetbyBusinessName(expectedName)).Returns(Task.FromResult<Business>(new Business { BusinessName = expectedName }));

            //Act
            var result = await controller.GetbyBusinessName(expectedName);
            var resultObj = result as OkObjectResult;

            //Asset
            mockRepo.Verify(repo => repo.GetbyBusinessName(expectedName), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

        [Theory]
        [InlineData("A Name")]
        [InlineData("")]
        public async void GetByBusinessName_ReturnsOkObjectResultWithCorrectBusiness(string name)
        {
            //Arrange
            string expectedName = name;
            mockRepo.Setup(repo => repo.GetbyBusinessName(expectedName)).Returns(Task.FromResult<Business>(new Business { BusinessName = expectedName }));

            //Act
            var result = await controller.GetbyBusinessName(name);
            var resultObj = result as OkObjectResult;
            var model = resultObj.Value as Business;

            //Asset
            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);

            Assert.Equal(expectedName, model.BusinessName);
        }

        [Fact]
        public async void GetByBusinessName_CallsGetProductsOnMockRepo_ReturnsOkObjectResultWithCorrectProductList()
        {
            //Arrange
            string businessName = "Business";
            int expectedId = 4;

            mockRepo.Setup(repo => repo.GetbyBusinessName(businessName)).Returns(Task.FromResult<Business>(new Business { Id = expectedId }));
            mockRepo.Setup(repo => repo.GetProducts(expectedId)).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>() { new Product { BusinessId = expectedId } }));

            //Act
            var result = await controller.GetbyBusinessName(businessName);
            var resultObj = result as OkObjectResult;
            var modelProducts = (resultObj.Value as Business).Products;

            //Asset
            mockRepo.Verify(repo => repo.GetProducts(expectedId), Times.Once);

            Assert.Equal(200, resultObj.StatusCode);
            Assert.NotNull(modelProducts);

            Assert.Equal(expectedId, modelProducts.First().BusinessId);
        }

        [Fact]
        public async void GetbyBusinessName_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            string businessName = "Errored Business";

            mockRepo.Setup(repo => repo.GetbyBusinessName(businessName)).Throws(new Exception());

            //Act
            var result = await controller.GetbyBusinessName(businessName);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
