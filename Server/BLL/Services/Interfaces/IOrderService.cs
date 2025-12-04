using BLL.DTO.Interfaces.Update;
using BLL.DTO.Objects.Order.Create;
using BLL.DTO.Objects.Order.Main;
using BLL.DTO.Objects.Order.Special;
using DAL.EfCore.Models;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderListDto>> GetOrdersAsync();

        Task<List<UserOrderListDto>> GetUserOrdersAsync(long id);

        Task<List<TileOrderListDto>> GetTileOrdersAsync(int id);

        Task<OrderFullDto> GetOrderFullByIdAsync(int id);

        Task<OrderInfoDto> GetOrderInfoByIdAsync(int id);

        Task<OrderFullDto> CreateOrderAsync(long userId, OrderCreateDto order);

        Task<bool> UpdateOrderAsync(int id, IUpdateDto<Order> order);

        Task<bool> DeleteOrderAsync(int id);
    }
}
