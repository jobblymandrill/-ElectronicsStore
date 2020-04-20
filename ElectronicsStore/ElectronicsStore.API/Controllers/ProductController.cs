using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public ProductOutputModel AddOrUpdateProduct(ProductInputModel inputModel)
        {
            return new ProductOutputModel();
        }

        [HttpDelete]
        public void DeleteProduct(string name)
        {

        }

        [HttpGet("search")]
        public ProductOutputModel SearchProduct(ProductInputModel inputModel)
        {
            return new ProductOutputModel();
        }

        [HttpGet("products/from-store-not-present-in-msc-and-spb")]
        public List<ProductOutputModel> GetProductsFromStoreNotPresentInMscAndSpb()
        {
            return new List<ProductOutputModel>();
        }

        [HttpGet("products/finished")]
        public List<ProductOutputModel> GetRanOutProducts()
        {
            return new List<ProductOutputModel>();
        }

        [HttpGet("products/most-popular-in-each-city")]
        public List<City_ProductOutputModel> GetMostPopularProductInEachCity()
        {
            return new List<City_ProductOutputModel>();
        }

        [HttpGet("products/never-ordered")]
        public List<ProductOutputModel> GetNeverOrderedProducts()
        {
            return new List<ProductOutputModel>();
        }
    }
}
