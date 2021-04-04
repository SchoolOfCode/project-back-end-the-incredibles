using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_UpdatebyProduct
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_UpdatebyProduct()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

        [Fact]
        public async void UpdatebyProduct_CallsUpdatebyProductOnMockRepo_ReturnsCreatedObjectResult()
        {
            //Arrange
            int productId = 1;
            var newProduct = new Product() { };

            mockRepo.Setup(repo => repo.UpdatebyProduct(newProduct)).Returns(Task.FromResult<Product>(newProduct));

            //Act
            var result = await controller.UpdatebyProduct(productId, newProduct);
            var resultObj = result as OkObjectResult;

            //Asset
            mockRepo.Verify(repo => repo.UpdatebyProduct(newProduct), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);
        }

        [Fact]
        public async void UpdatebyProduct_UpdatesValuesForProductWithCorrectId()
        {
            //Arrange
            int expectedId = 1;
            var newProduct = new Product() { };

            mockRepo.Setup(repo => repo.UpdatebyProduct(newProduct)).Returns(Task.FromResult<Product>(newProduct));

            //Act
            var result = await controller.UpdatebyProduct(expectedId, newProduct);
            var resultObj = result as OkObjectResult;
            var updatedProduct = resultObj.Value as Product;

            //Asset
            Assert.NotNull(updatedProduct);
            Assert.Equal(expectedId, updatedProduct.ProductId);
        }

        [Fact]
        public async void UpdatebyProduct_ReturnsResultWithProductFieldsUpdated()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Product() { ProductName = "updated Product" };

            mockRepo.Setup(repo => repo.UpdatebyProduct(expectedProduct)).Returns(Task.FromResult<Product>(expectedProduct));

            //Act
            var result = await controller.UpdatebyProduct(productId, expectedProduct);
            var resultObj = result as OkObjectResult;
            var actualProduct = resultObj.Value as Product;

            //Asset
            Assert.Equal(expectedProduct.ProductName, actualProduct.ProductName);
        }

        [Fact]
        public async void UpdatebyProduct_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            int productId = 4;
            var newProduct = new Product() { };

            mockRepo.Setup(repo => repo.UpdatebyProduct(newProduct)).Throws(new Exception());

            //Act
            var result = await controller.UpdatebyProduct(productId, newProduct);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
