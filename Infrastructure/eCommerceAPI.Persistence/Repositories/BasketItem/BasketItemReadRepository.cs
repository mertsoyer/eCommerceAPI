using eCommerceAPI.Domain.Entities;
using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Persistence.Contexts;

namespace eCommerceAPI.Persistence.Repositories
{
    public class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(eCommerceAPIDbContext context) : base(context)
        {
        }
    }
}
