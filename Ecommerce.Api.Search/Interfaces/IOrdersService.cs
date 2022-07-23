using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrders(int customerId);
    }
}