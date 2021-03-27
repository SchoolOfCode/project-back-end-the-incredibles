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
    }
}
