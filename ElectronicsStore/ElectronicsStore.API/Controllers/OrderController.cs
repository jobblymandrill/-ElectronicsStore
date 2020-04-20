using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public OrderOutputModel AddOrUpdateOrder(OrderInputModel inputModel)
        {
            return new OrderOutputModel();
        }

        [HttpGet("search")]
        public OrderOutputModel SearchOrder(OrderInputModel inputModel)
        {
            return new OrderOutputModel();
        }
    }
}
