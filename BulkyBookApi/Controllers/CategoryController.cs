using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net.Http;

namespace BulkyBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IUnitOfWork db, ILogger<CategoryController> logger)
        {
            _unitOfWork = db;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAllCategory()
        {
            _logger.LogInformation("Get all category");
            var categoryList = _unitOfWork.Category.GetAll().ToList();
            return categoryList;
        }

        [HttpGet("GetAddedValue/{id1}/{id2}")]
        public int GetAddedValue(int id1, int id2)
        {
            var id3 = id1 + id2;
            return id3;
        }

        [HttpGet("GetSingleCategory/{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var category = _unitOfWork.Category.Get(c => c.Id == id);
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> AddNewCategory(Category newCat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Category.Add(newCat);
            _unitOfWork.Save();
            return Ok(newCat);
        }

        [HttpPost("UpdateCategoryById/{id}")]
        public ActionResult<Category> UpdateCategory(Category category, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = _unitOfWork.Category.Get(i => i.Id == id);

            if (model == null)
            {
                return BadRequest(ModelState);
            }

            model.Id = category.Id;
            model.DisplayOrder = category.DisplayOrder;
            model.Name = category.Name;

            _unitOfWork.Save();
            return Ok(model);
        }

        [HttpDelete("DeleteCategoryById/{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            var model = _unitOfWork.Category.Get(i => i.Id == id);

            if (model == null)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.Category.Remove(model);
            _unitOfWork.Save();
            return Ok(model);
        }

        [HttpPost("upload")] 
        public async Task<IActionResult> UploadFile() 
        {
            if (!Request.HasFormContentType || !Request.Form.Files.Any()) 
            { 
                return BadRequest("Invalid file upload."); 
            }
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads"); 
            if (!Directory.Exists(uploadsFolderPath)) 
            { 
                Directory.CreateDirectory(uploadsFolderPath); 
            }
            foreach (var file in Request.Form.Files) 
            { 
                var filePath = Path.Combine(uploadsFolderPath, file.FileName); 
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                { 
                    await file.CopyToAsync(fileStream); 
                } 
            }

            return Ok("File uploaded successfully.");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
