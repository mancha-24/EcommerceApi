using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;
        private readonly ILogger<SearchService> _logger;
        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService, ILogger<SearchService> logger)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersService = customersService;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrders(customerId);
            var productsResult = await _productsService.GetProducts();
            var customersResult = await _customersService.GetCustomers(customerId);

            if(ordersResult.isSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.isSuccess ? 
                            productsResult.Products.FirstOrDefault(p => p.Id == item.Id)?.Name :
                            "Product information is not available";
                    }
                }
                var result = new
                {
                    Customer = customersResult.isSuccess ? 
                                customersResult.Customer :
                                new { Name = "Customer information is not available"},
                    Order = ordersResult.Orders
                };
                _logger?.LogInformation("Successfull Return");
                return (true, result);
            }
            _logger?.LogInformation("Retornando 404 Notfound");
            return (false, null);
        }
    }
}