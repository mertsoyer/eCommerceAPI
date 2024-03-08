using eCommerceAPI.Application.ViewModels.Basket;
using eCommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddITemToBasketAsync(BasketItemCreateViewModel basketItem);
        public Task UpdateQuantityAsync(UpdateBasketItemViewModel basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
    }
}
