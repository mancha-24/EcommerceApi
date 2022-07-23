using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Tests;

public class ProductsTest
{
     ProductsProvider? _productsProvider;
    public ProductsTest()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
                        .UseInMemoryDatabase(nameof(GetProdutsReturnAllProducts)).Options;
                        
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();        
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);
        _productsProvider = new ProductsProvider(dbContext, null, mapper);
    }
    [Fact]
    public async Task GetProdutsReturnAllProducts()
    {
        var product = await _productsProvider.GetProductsAsync();
        Assert.True(product.IsSuccess);
        Assert.True(product.Products.Any());
        Assert.Null(product.ErrorMessage);

    }

    [Fact]
    public async Task GetProdutsReturnProductUsingValidId()
    {
        var product = await _productsProvider.GetProductAsync(1);

        Assert.True(product.IsSuccess);
        Assert.NotNull(product.Product);
        Assert.True(product.Product.Id == 1);
        Assert.Null(product.ErrorMessage);
    }

    [Fact]
    public async Task GetProdutsReturnProductUsingInvalidId()
    {
        var product = await _productsProvider.GetProductAsync(-1);

        Assert.False(product.IsSuccess);
        Assert.Null(product.Product);
        Assert.NotNull(product.ErrorMessage);
    }

    private void CreateProducts(ProductsDbContext dbContext)
    {
        if (!dbContext.Products.Any())
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }      
            dbContext.SaveChanges();  
        }
    }
}