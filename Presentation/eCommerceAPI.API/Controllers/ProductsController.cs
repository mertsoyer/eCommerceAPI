using eCommerceAPI.Application.Features.Commands.Product.CreateProduct;
using eCommerceAPI.Application.Features.Commands.Product.DeleteProduct;
using eCommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using eCommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using eCommerceAPI.Application.Features.Queries.Product.GetProductById;
using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Application.RequestParameters;
using eCommerceAPI.Application.Services;
using eCommerceAPI.Application.ViewModels.Product;
using eCommerceAPI.Domain.Entities;
using MediatR;
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
        private readonly IMediator _mediator;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IFileService fileService, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _fileService = fileService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            //var totalCount = _productReadRepository.GetAll().Count();
            //var datas = _productReadRepository.GetAll().Select(x => new
            //{
            //    x.Id,
            //    x.Name,
            //    x.Stock,
            //    x.Price,
            //}).Skip(pagination.Size * pagination.Page).Take(pagination.Size).ToList();
            //return Ok(new { datas, totalCount });

            var datas = await _mediator.Send(getAllProductQueryRequest);
            return Ok(datas);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetProductByIdQueryRequest getbyIdProductQueryRequest)
        {
            var data = await _mediator.Send(getbyIdProductQueryRequest);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            #region eski versiyon
            //await _productWriteRepository.AddAsync(new Product
            //{
            //    Name = model.Name,
            //    Price = model.Price,
            //    Stock = model.Stock,
            //});
            //await _productWriteRepository.SaveAsync(); 
            #endregion

            var response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {

            await _mediator.Send(deleteProductCommandRequest);

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
