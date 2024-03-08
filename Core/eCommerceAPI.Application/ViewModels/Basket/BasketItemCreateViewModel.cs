using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.ViewModels.Basket
{
    public class BasketItemCreateViewModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
