using ChannelEngine.Shared.Models;

namespace ChannelEngine.Shared.Services;

public interface IChannelEngineService
{
    Task<List<OrderDto>> GetInProgressOrdersAsync();
    Task<List<OrderDto>> GetTop5ProductsAsync();
    Task<bool> SetProductStockAsync(string merchantProductNo, int stock);
}
