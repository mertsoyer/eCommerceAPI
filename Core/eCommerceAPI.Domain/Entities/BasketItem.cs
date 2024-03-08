﻿using eCommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Domain.Entities
{
    /// <summary>
    /// Sepetin içinde yer alan ürünler
    /// </summary>
    public class BasketItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
