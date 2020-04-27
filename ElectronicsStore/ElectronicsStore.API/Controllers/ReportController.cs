using AutoMapper;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase, IReportController
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        public ReportController(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        [HttpGet("category/with-more-than-{number}-products")]//works but sql procedure need to be refactored
        public async ValueTask<ActionResult<List<CategoryWithNumberOutputModel>>> GetCategoriesWithACertainProductNumber(int number)
        {
            var result = await _reportRepository.GetCategoriesWithACertainProductNumber(number);
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no categories with this number of products.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<CategoryWithNumberOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/from-warehouse-not-present-in-msc-and-spb")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetProductsFromWareHouseNotPresentInMscAndSpb()//works
        {
            var result = await _reportRepository.GetProductsFromWareHouseNotPresentInMscAndSpb();
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no products from warehouse not present in Msc and Spb.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/finished")]
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetRanOutProducts()//works
        {
            var result = await _reportRepository.GetRanOutProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null)
                {
                    return Problem($"There is no ran out products.", statusCode: 520);
                }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("product/most-popular-in-each-city")]
        public List<City_ProductOutputModel> GetMostPopularProductInEachCity()
        {
            return new List<City_ProductOutputModel>();
        }

        [HttpGet("product/never-ordered")]//works
        public async ValueTask<ActionResult<List<ProductOutputModel>>> GetNeverOrderedProducts()
        {
            var result = await _reportRepository.GetNeverOrderedProducts();
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"List of never ordered products is empty.", statusCode: 520); }
                return Ok(_mapper.Map<List<ProductOutputModel>>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //[HttpGet("income/from-russia-and-foreign-countries")]
        //public List<IncomeOutputModel> GetIncomeFromRussiaAndFromForeignCountries()
        //{
        //    return new List<IncomeOutputModel>();
        //}

        //[HttpGet("income/from-each-filial")]
        //public List<IncomeOutputModel> GetIncomeFromEachFilial()
        //{
        //    return new List<IncomeOutputModel>();
        //}

        //[HttpGet("income/of-filial-during-period")]
        //public decimal GetIncomeOfFilialDuringPeriod(string startDate, string finishDate, string name)
        //{
        //    return 10000000;
        //}
    }
}
