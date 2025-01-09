using BulkyBookApi.Controllers;
using Bulky.DataAccess.Data;
using Moq;
//using NLog;
using Microsoft.AspNetCore.Identity;
using NLog.Web;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Linq.Expressions;

namespace Tests.BulkyBook.Api
{
    [TestClass]
    public sealed class CategoryTests
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private CategoryController _controller;
        //private readonly ILogger<CategoryController> _logger;

        //public CategoryTests()
        //{
            
        //}

        //public CategoryTests(IUnitOfWork unitOfWork, ILogger<CategoryController> logger, CategoryController controller)
        //{
        //    _unitOfWork = unitOfWork;
        //    _logger = logger;
        //    _controller = controller;
        //}

        //[TestInitialize]
        //public void BeforeTest()
        //{
            
        //}

        //[TestMethod]
        //public void GetAddedValueTests()
        //{
        //    var id1 = 1;
        //    var id2 = 6;
        //    var expected = id1 + id2;
        //    var output = _controller.GetAddedValue(id1, id2);

        //    Assert.AreEqual(output, expected);
        //}

        [TestMethod]
        public void GetSingleCategoryTests()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<CategoryController>>();
            var testCategory = new Category { Id = 1, Name = "Test Category" };

            mockUnitOfWork.Setup(uow => uow.Category.Get(It.IsAny<Expression<Func<Category, bool>>>()))
                .Returns(testCategory);

            var controller = new CategoryController(mockUnitOfWork.Object, mockLogger.Object);

            // Act
            var result = controller.GetCategoryById(1).Result as OkObjectResult;
            var category = result.Value as Category;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(category);
            Assert.AreEqual(1, category.Id);
            Assert.AreEqual("Test Category", category.Name);
        }

        [TestMethod]
        public void GetAllCategoryTests()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<CategoryController>>();
            var testCategory = new List<Category>();

            mockUnitOfWork.Setup(uow => uow.Category.GetAll())
                .Returns(testCategory);

            var controller = new CategoryController(mockUnitOfWork.Object, mockLogger.Object);

            // Act
            var result = controller.GetAllCategory().Result as OkObjectResult;
            var category = result.Value as List<Category>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(category);
        }
    }
}
