using AutoMapper;
using ElecronicsStore.DB.Models;
using ElectronicsStore.API.Models.InputModels;
using ElectronicsStore.API.Models.OutputModels;
using ElectronicsStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, IOrderController
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async ValueTask<ActionResult<long>> AddOrUpdateOrder(OrderInputModel inputModel)
        {
            var result = await _orderRepository.AddOrUpdateOrder(_mapper.Map<Order>(inputModel));
            if (result.IsOkay)
            {
                //if (result.RequestData == null) { return NotFound("Operation wasnt executed"); }
                return Ok(/*_mapper.Map<OrderOutputModel>*/(result.RequestData));
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
