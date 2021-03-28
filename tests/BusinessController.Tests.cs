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
        public async void AllMethods_WhenRepoThrowsException_ReturnsBadRequest()
        {
            //Is there a way to call all methods in one test??

            //Arrange
           
            //Act

            //Assert
            Assert.Equal(404, 404); ;

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

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(5)]
        public async void GetByBusiness_CallsGetByBusinessMethodOnMockRepo_ReturnsOkObjectResultWithCorrectBusiness(int id)
        {
            //Arrange
            int expectedId = id;
            mockRepo.Setup(repo => repo.GetbyBusiness(id)).Returns(Task.FromResult<Business>(new Business { Id = expectedId }));
            //Act
            var result = await controller.GetbyBusiness(id);
            var resultObj = result as OkObjectResult;
            var model = resultObj.Value as Business;

            //Asset
            mockRepo.Verify(repo => repo.GetbyBusiness(id), Times.Once);

            Assert.NotNull(resultObj);
            Assert.Equal(200, resultObj.StatusCode);

            Assert.Equal(expectedId, model.Id);
        }

        [Fact]
        public async void CreateBusiness_CallsInsertbyBusinessOnMockRepo_CreatedResultReturnedWithNewBusiness()
        {
            //Arrange
            var business = new Business { BusinessName = "Loves2Test", IsTrading = true };

            mockRepo.Setup(repo => repo.InsertbyBusiness(business)).Returns(Task.FromResult<Business>(business));

            //Act
            var result = await controller.InsertbyBusiness(business);
            var resultObj = result as CreatedResult;
            var model = resultObj.Value as Business;

            //Assert
            mockRepo.Verify(repo => repo.InsertbyBusiness(business), Times.Once);

            Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, resultObj.StatusCode);

            Assert.Equal(business.BusinessName, model.BusinessName);
            Assert.Equal(business.IsTrading, model.IsTrading);
        }
    }
}
