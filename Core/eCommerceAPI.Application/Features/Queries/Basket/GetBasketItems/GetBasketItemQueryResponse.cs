using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemQueryResponse
    {
        public string BasketItemId { get; set; } //Basket Item Id 
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
