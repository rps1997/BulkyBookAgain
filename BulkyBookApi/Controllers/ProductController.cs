using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace BulkyBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IUnitOfWork db, ILogger<ProductController> logger)
        {
            _unitOfWork = db;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProduct()
        {
            _logger.LogInformation("Get all products");
            var ProductList = _unitOfWork.Product.GetAll().ToList();
            return ProductList;
        }

        [HttpGet("GetAddedValue/{id1}/{id2}")]
        public ActionResult<int> GetAddedValue(int id1, int id2)
        {
            var id3 = id1 + id2;
            return Ok(id3);
        }

        [HttpGet("GetSingleProduct/{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var Product = _unitOfWork.Product.Get(c => c.Id == id);
            return Ok(Product);
        }

        [HttpPost]
        public ActionResult<Product> AddNewProduct(Product newCat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Product.Add(newCat);
            _unitOfWork.Save();
            return Ok(newCat);
        }

        [HttpPost("UpdateProductById/{id}")]
        public ActionResult<Product> UpdateProduct(Product product, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _unitOfWork.Product.Get(i => i.Id == id);

            if (model == null)
            {
                return BadRequest(ModelState);
            }

            model.Id = product.Id;
            model.Author = product.Author;
            model.ISBN = product.ISBN;
            model.Description = product.Description;
            model.ListPrice = product.ListPrice;
            model.Price = product.Price;
            model.Price50 = product.Price50;
            model.Price100 = product.Price100;
            model.CategoryId = product.CategoryId;
            model.ImageUrl = product.ImageUrl;
            model.Title = product.Title;

            _unitOfWork.Save();
            return Ok(model);
        }

        [HttpDelete("DeleteProductById/{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var model = _unitOfWork.Product.Get(i => i.Id == id);

            if (model == null)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Product.Remove(model);
            _unitOfWork.Save();
            return Ok(model);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
