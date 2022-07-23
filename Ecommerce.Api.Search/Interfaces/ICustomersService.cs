namespace Ecommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool isSuccess, dynamic Customer, string ErrorMessage)> GetCustomers(int id);
    }
}