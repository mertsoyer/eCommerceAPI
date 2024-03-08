using eCommerceAPI.Application.Features.Commands.Basket.AddItemToBasket;
using eCommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem;
using eCommerceAPI.Application.Features.Commands.Basket.UpdateQuantity;
using eCommerceAPI.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sepetteki ürünleri getirir
        /// </summary>
        /// <param name="getBasketItemQueryRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemQueryRequest getBasketItemQueryRequest)
        {
            var response = await _mediator.Send(getBasketItemQueryRequest);
            return Ok(response);
        }

        /// <summary>
        /// Sepete ürün ekler
        /// </summary>
        /// <param name="getBasketItemQueryRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketRequest addItemToBasketRequest)
        {
            var response = await _mediator.Send(addItemToBasketRequest);
            return Ok(response);
        }

        /// <summary>
        /// Sepetteki ürün miktarını günceller
        /// </summary>
        /// <param name="updateQuantityCommandRequest"></param>
        /// <returns></returns>
        [HttpPut/*("{BasketItemId}")*/]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
            var response = await _mediator.Send(updateQuantityCommandRequest);
            return Ok(response);
        }

        /// <summary>
        /// Sepetten ürün siler
        /// </summary>
        /// <param name="removeBasketItemCommandRequest"></param>
        /// <returns></returns>
        [HttpDelete("{BasketItemId}")] //api/basket/BasketItemId
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            var response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}
