using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Services
{
    public class ProductService : IProductsService
    {
        IHttpClientFactory _httpClientFactory;
        ILogger<ProductService> _logger;
        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool isSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProducts()
        {
            try     
            {
                var client = _httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync($"api/products");

                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

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