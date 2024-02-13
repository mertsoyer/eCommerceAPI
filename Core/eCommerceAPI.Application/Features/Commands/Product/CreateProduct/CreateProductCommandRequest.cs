using eCommerceAPI.Application.ViewModels.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        // Bu şekilde property tanımlayıp property üzerinden de gidilebilir.  ProductCreateViewModel.Name gibi
        //public ProductCreateViewModel ProductCreateViewModel { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
