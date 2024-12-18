using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitofWork_Pattern_Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IRepository<Category> categoryRepository;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            categoryRepository = new CategoryRepository(_unitOfWork);
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await categoryRepository.Get();
            return categories;
        }
        [Route("CreateCategory")]
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var newCategory = await categoryRepository.Create(category);
            return newCategory;
        }

        [HttpDelete("{id}")]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categories = await categoryRepository.Delete(id);
            return categories;
        }

        [HttpPut("{id}")]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = await categoryRepository.Update(id, category);
            return categoryToUpdate;
        }
    }
}
