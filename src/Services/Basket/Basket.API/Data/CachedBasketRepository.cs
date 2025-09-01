
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository (IBasketRepository repository,IDistributedCache cache)
        : IBasketRepository
    {
       

        public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShopingCart>(cachedBasket);
            var basket=await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }

        public async Task<ShopingCart> StoreBasket(ShopingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
        public async Task<bool> DeleteBaslet(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBaslet(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }
    }
}
