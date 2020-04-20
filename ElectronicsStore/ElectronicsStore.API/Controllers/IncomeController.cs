using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        [HttpGet("from-russia-and-foreign-countries")]
        public List<IncomeOutputModel> GetIncomeFromRussiaAndFromForeignCountries()
        {
            return new List<IncomeOutputModel>();
        }

        [HttpGet("from-each-filial")]
        public List<IncomeOutputModel> GetIncomeFromEachFilial()
        {
            return new List<IncomeOutputModel>();
        }

        [HttpGet("of-filial-during-period")]
        public decimal GetIncomeOfFilialDuringPeriod(string startDate, string finishDate, string name)
        {
            return 10000000;
        }
    }
}
