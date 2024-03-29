﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.Basket.AddItemToBasket
{
    public class AddItemToBasketRequest : IRequest<AddItemToBasketResponse>
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
