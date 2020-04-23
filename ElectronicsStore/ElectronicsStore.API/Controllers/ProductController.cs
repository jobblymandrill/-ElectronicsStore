using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("search")]
        public ProductOutputModel SearchProduct(ProductInputModel inputModel)
        {
            return new ProductOutputModel();
        }
    }
}
