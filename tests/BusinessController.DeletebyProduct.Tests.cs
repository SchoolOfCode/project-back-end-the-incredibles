using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace tests
{
    public class BusinessControllerTests_DeletebyProduct
    {
        //Set up for all tests
        private readonly Mock<IRepository<Business>> mockRepo;

        private readonly BusinessController controller;

        public BusinessControllerTests_DeletebyProduct()
        {
            mockRepo = new Mock<IRepository<Business>>();
            controller = new BusinessController(mockRepo.Object);
        }

       
    }
}
