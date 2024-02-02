using eCommerceAPI.Application.Abstractions;
using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace eCommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController( IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            //var data = _productService.GetProducts();
            //return Ok(data);
            await _productWriteRepository.AddRangeAsync(new List<Product>()
            {
                new Product{Id=Guid.NewGuid(),Name="Product 1",Price=100,CreatedDate=DateTime.Now,Stock=10},
                new Product{Id=Guid.NewGuid(),Name="Product 1",Price=200,CreatedDate=DateTime.Now,Stock=20},
                new Product{Id=Guid.NewGuid(),Name="Product 1",Price=300,CreatedDate=DateTime.Now,Stock=30},
            });
            await _productWriteRepository.SaveAsync();
        }
    }
}
