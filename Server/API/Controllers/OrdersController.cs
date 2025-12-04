using BLL.DTO.Objects.Order.Create;
using BLL.DTO.Objects.Order.Update;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _service.GetOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(long userId)
        {
            try
            {
                var orders = await _service.GetUserOrdersAsync(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tile/{tileId}")]
        public async Task<IActionResult> GetUserOrders(int tileId)
        {
            try
            {
                var orders = await _service.GetTileOrdersAsync(tileId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderFull(int id)
        {
            try
            {
                var order = await _service.GetOrderFullByIdAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> GetOrderInfo(int id)
        {
            try
            {
                var order = await _service.GetOrderInfoByIdAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user/{userId}/create")]
        public async Task<IActionResult> PostOrder(long userId, OrderCreateDto order)
        {
            try
            {
                var createdOrder = await _service.CreateOrderAsync(userId, order);
                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> PutOrder(int id, MasterOrderUpdateDto order)
        {
            try
            {
                var result = await _service.UpdateOrderAsync(id, order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _service.DeleteOrderAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}