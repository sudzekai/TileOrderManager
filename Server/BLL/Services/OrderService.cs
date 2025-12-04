using BLL.DTO.Interfaces.Special;
using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Order.Create;
using BLL.DTO.Objects.Order.Main;
using BLL.DTO.Objects.Order.Special;
using BLL.DTO.Tools;
using BLL.Services.Interfaces;
using DAL.EfCore.Models;
using DAL.EfCore.UOW.Interface;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<OrderListDto>> GetOrdersAsync()
        {
            var orders = await _uow.Orders.GetAllAsync();
            return [.. orders.Select(o => o.ToDto<Order, OrderListDto>())];
        }

        public async Task<List<UserOrderListDto>> GetUserOrdersAsync(long id)
        {
            var orders = await _uow.Orders.GetByUserIdAsync(id);
            return [.. orders.Select(o => o.ToDto<Order, UserOrderListDto>())];
        }

        public async Task<List<TileOrderListDto>> GetTileOrdersAsync(int id)
        {
            var orders = await _uow.Orders.GetByTileIdAsync(id);
            return [.. orders.Select(o => o.ToDto<Order, TileOrderListDto>())];
        }

        public async Task<OrderFullDto> GetOrderFullByIdAsync(int id)
            => await GetOrderAsync<OrderFullDto>(id);

        public async Task<OrderInfoDto> GetOrderInfoByIdAsync(int id)
            => await GetOrderAsync<OrderInfoDto>(id);

        private async Task<T> GetOrderAsync<T>(int id)
            where T : ISpecialDto<Order>, new()
        {
            var order = await _uow.Orders.GetByIdAsync(id);

            return order == null ? throw new Exception($"Заказ с Id {id} не найден")
                                 : order.ToDto<Order, T>();
        }

        public async Task<OrderFullDto> CreateOrderAsync(long userId, OrderCreateDto order)
        {
            var createdOrder = order.ToModel();

            createdOrder.UserId = userId;

            await _uow.Orders.AddAsync(createdOrder);
            await _uow.SaveChagesAsync();

            return createdOrder.ToDto<Order, OrderFullDto>();
        }

        public async Task<bool> UpdateOrderAsync(int id, IUpdateDto<Order> order)
        {
            var foundOrder = await _uow.Orders.GetByIdAsync(id) ?? throw new Exception($"Заказ с Id {id} не найден");

            foundOrder.UpdateModel(order);
            await _uow.SaveChagesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var result = await _uow.Orders.DeleteAsync(id);

            if (result)
                await _uow.SaveChagesAsync();

            return result;
        }
    }
}
