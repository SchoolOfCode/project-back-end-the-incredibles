using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_InsertbyProduct
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_InsertbyProduct()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }
        [Fact]
        public async void InsertbyProduct_CallsInsertbyProductOnMockRepo_ReturnsCreatedObjectResult()
        {
            //Arrange
            var expectedProduct = new Product() { };

            mockRepo.Setup(repo => repo.InsertbyProduct(expectedProduct)).Returns(Task.FromResult<Product>(new Product { }));

            //Act
            var result = await controller.InsertbyProduct(expectedProduct);
            var resultObj = result as CreatedResult;

            //Asset
            mockRepo.Verify(repo => repo.InsertbyProduct(expectedProduct), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(201, resultObj.StatusCode);
        }

        [Fact]
        public async void InsertbyProduct_ReturnsCreatedObjectResultForNewProductWithCorrectValues()
        {
            //Arrange
            var expectedProduct = new Product() { ProductName = "new product", ProductPrice= 4.56m};

            mockRepo.Setup(repo => repo.InsertbyProduct(expectedProduct)).Returns(Task.FromResult<Product>(expectedProduct));

            //Act
            var result = await controller.InsertbyProduct(expectedProduct);
            var resultObj = result as CreatedResult;
            var newProduct = resultObj.Value as Product;

            //Asset
            Assert.NotNull(newProduct);
            Assert.Equal(expectedProduct.ProductName, newProduct.ProductName);
            Assert.Equal(expectedProduct.ProductPrice, newProduct.ProductPrice);
        }

        [Fact]
        public async void InsertbyProduct_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Arrange
            var newProduct = new Product() { };

            mockRepo.Setup(repo => repo.InsertbyProduct(newProduct)).Throws(new Exception());

            //Act
            var result = await controller.InsertbyProduct(newProduct);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
