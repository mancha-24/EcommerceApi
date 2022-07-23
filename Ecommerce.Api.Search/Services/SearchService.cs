using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        public SearchService(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrders(customerId);

            if(ordersResult.isSuccess)
            {
                var result = new
                {
                    Order = ordersResult.Orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}