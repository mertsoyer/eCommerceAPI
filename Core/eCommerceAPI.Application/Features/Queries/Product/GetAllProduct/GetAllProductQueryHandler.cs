using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var datas = _productReadRepository.GetAll().Select(x => new ProductResponseModel
            {

                Id = x.Id,
                Name = x.Name,
                Stock = x.Stock,
                Price = x.Price,
            }).Skip(request.Pagination.Size * request.Pagination.Page).Take(request.Pagination.Size).ToList();

            return new GetAllProductQueryResponse
            {
                TotalCount = totalCount,
                Products = datas
            };
        }
    }
}
