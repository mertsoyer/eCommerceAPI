using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Application.RequestParameters;
using eCommerceAPI.Application.Services;
using eCommerceAPI.Application.ViewModels.Product;
using eCommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace eCommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IFileService _fileService;


        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var datas = _productReadRepository.GetAll().Select(x => new
            {
                x.Id,
                x.Name,
                x.Stock,
                x.Price,
            }).Skip(pagination.Size * pagination.Page).Take(pagination.Size).ToList();
            return Ok(datas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = _productReadRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductCreateViewModel model)
        {
            await _productWriteRepository.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductUpdateViewModel model)
        {
            var entity = await _productReadRepository.GetByIdAsync(model.Id);
            entity.Stock = model.Stock;
            entity.Price = model.Price;
            entity.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        //[HttpPost("Upload")]
        [HttpPost("[action]")] // -> [action] yazılarak direkt metot adı olan Upload ı alıyor
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            return Ok();
        }

    }
}
