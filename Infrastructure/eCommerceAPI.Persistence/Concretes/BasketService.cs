using eCommerceAPI.Application.Abstractions.Services;
using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Application.ViewModels.Basket;
using eCommerceAPI.Domain.Entities;
using eCommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly UserManager<AppUser> _userManager;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketReadRepository = basketReadRepository;
        }

        /// <summary>
        /// _httpContextAccessor aracılığı ile o esnadaki giriş yapmış kullanıcının UserName i elde edilir 
        /// </summary>
        /// <returns></returns>
        private async Task<Basket?> ContextUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                var user = await _userManager.Users
                              .Include(x => x.Baskets)
                              .FirstOrDefaultAsync(x => x.UserName == userName);

                //var _basket = from basket in user.Baskets
                //              join order in _orderReadRepository.Table
                //              on basket.Id equals order.Id into BasketOrders
                //              from order in BasketOrders.DefaultIfEmpty()
                //              select new
                //              {
                //                  Basket=basket,
                //                  Order=order
                //              };

                var _basket = user?.Baskets.Join(
    _orderReadRepository.Table,
    basket => basket.Id,
    order => order.Id,
    (basket, order) => new
    {
        Basket = basket,
        Order = order
    })
.DefaultIfEmpty().ToList();


                Basket? targetBasket = null;
                if (_basket.Any(x => x.Order is null))
                {
                    targetBasket = _basket.FirstOrDefault(x => x.Order is null)?.Basket;
                }
                else
                {
                    targetBasket = new Basket();
                    user.Baskets.Add(targetBasket);
                    await _basketWriteRepository.SaveAsync();
                }
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hata ile karşılaşıldı");
        }

        public async Task AddITemToBasketAsync(BasketItemCreateViewModel basketItem)
        {
            var basket = await ContextUser();
            if (basket != null)
            {
                var _basketItem = await _basketItemReadRepository.GetSingleAsync(x => x.BasketId == basket.Id && x.ProductId == Guid.Parse(basketItem.ProductId));
                //Eğer bu ürün daha evvelden ekliyse ürün miktarını arttır/güncelle
                if (_basketItem != null)
                {
                    _basketItem.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new BasketItem
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                }
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            var basket = await ContextUser();
            var result = _basketReadRepository.Table.Include(x => x.BasketItems).ThenInclude(y => y.Product).FirstOrDefault(x => x.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            var _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (_basketItem != null)
            {
                _basketItemWriteRepository.Remove(_basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemViewModel basketItem)
        {
            var _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);

            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();
            }
        }
    }
}
