using eCommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketRequest, AddItemToBasketResponse>
    {
        private readonly IBasketService _basketService;

        public AddItemToBasketCommandHandler(IBasketService basketServic)
        {
            _basketService = basketServic;
        }

        public async Task<AddItemToBasketResponse> Handle(AddItemToBasketRequest request, CancellationToken cancellationToken)
        {
            await _basketService.AddITemToBasketAsync(new ViewModels.Basket.BasketItemCreateViewModel
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            });

            return new AddItemToBasketResponse();
        }
    }
}
