using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ElectronicsStore.API.Controllers
{
    public class ReportController
    {
        [HttpGet("category/with-more-than-{number}-products")]
        public List<CategoryOutputModel> GetCategoriesWithACertainProductNumber(int number)
        {
            return new List<CategoryOutputModel>();
        }

        [HttpGet("product/from-store-not-present-in-msc-and-spb")]
        public List<ProductOutputModel> GetProductsFromStoreNotPresentInMscAndSpb()
        {
            return new List<ProductOutputModel>();
        }

        [HttpGet("product/finished")]
        public List<ProductOutputModel> GetRanOutProducts()
        {
            return new List<ProductOutputModel>();
        }

        //[HttpGet("product/most-popular-in-each-city")]
        //public List<City_ProductOutputModel> GetMostPopularProductInEachCity()
        //{
        //    return new List<City_ProductOutputModel>();
        //}

        [HttpGet("product/never-ordered")]
        public List<ProductOutputModel> GetNeverOrderedProducts()
        {
            return new List<ProductOutputModel>();
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

        [HttpGet("income/of-filial-during-period")]
        public decimal GetIncomeOfFilialDuringPeriod(string startDate, string finishDate, string name)
        {
            return 10000000;
        }
    }
}
