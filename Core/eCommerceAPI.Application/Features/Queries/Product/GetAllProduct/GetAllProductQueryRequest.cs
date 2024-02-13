using eCommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public Pagination Pagination { get; set; }

        //Bu şekilde de karşılanabilirdi
        //public int Page { get; set; } = 0; //default değer
        //public int Size { get; set; } = 5; //default değer
    }
}
