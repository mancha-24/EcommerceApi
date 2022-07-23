using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool isSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProducts();
    }
}