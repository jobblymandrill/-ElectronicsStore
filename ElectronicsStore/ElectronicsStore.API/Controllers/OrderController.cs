using AutoMapper;
using ElecronicsStore.API;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Core.ConfigurationOptions;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOptions<UrlOptions> _urlOptions;
        public OrderController(IOrderRepository orderRepository, IMapper mapper, IOptions<UrlOptions> urlOptions)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _urlOptions = urlOptions;
        }

        [HttpPost]
        public async ValueTask<ActionResult<OrderOutputModel>> AddOrUpdateOrder(OrderInputModel inputModel)//add works, update doesnt
        {
            var result = await _orderRepository.AddOrUpdateOrder(_mapper.Map<Order>(inputModel));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return NotFound("Operation wasnt executed"); }
                return Ok(_mapper.Map<OrderOutputModel>(result.RequestData));
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        //[HttpGet("search")]
        //public OrderOutputModel SearchOrder(OrderInputModel inputModel)
        //{
        //    return new OrderOutputModel();
        //}
    }
}
