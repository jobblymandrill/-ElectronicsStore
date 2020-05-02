using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase, IProductController
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("search")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> SearchProduct(ProductSearchInputModel inputModel)
        {
            var result = await _productRepository.SearchProduct(_mapper.Map<ProductSearch>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Product(s) cant be found"); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }
    }
}
