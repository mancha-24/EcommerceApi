using System.Text.Json;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Services
{
    public class OrdersService : IOrdersService
    {
        IHttpClientFactory _httpClientFactory;
        ILogger<OrdersService> _logger;
        public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool isSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrders(int customerId)
        {
            try     
            {
                var client = _httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/orders/{customerId}");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                    return (true, result, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}