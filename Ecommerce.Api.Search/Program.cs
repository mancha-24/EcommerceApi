using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddHttpClient("OrderService", config =>{
    config.BaseAddress = new Uri(configuration["Services:Orders"]);
});

builder.Services.AddHttpClient("ProductsService", config =>{
    config.BaseAddress = new Uri(configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(p=> p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("CustomersService", config =>{
    config.BaseAddress = new Uri(configuration["Services:Customers"]);
}).AddTransientHttpErrorPolicy(p=> p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
