using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpPost]
        public CategoryOutputModel AddOrUpdateCategory(CategoryInputModel inputModel)
        {
            return new CategoryOutputModel();
        }

        [HttpDelete]
        public void DeleteCategory(string name)
        {

        }

        [HttpGet("search")]
        public CategoryOutputModel SearchCategory(CategoryInputModel inputModel)
        {
            return new CategoryOutputModel();
        }

        [HttpGet("with-more-than-{number}-products")]
        public List<CategoryOutputModel> GetCategoriesWithACertainProductNumber(int number)
        {
            return new List<CategoryOutputModel>();
        }
    }
}
