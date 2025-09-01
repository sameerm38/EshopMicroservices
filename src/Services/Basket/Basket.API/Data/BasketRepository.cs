

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
      

        public async Task<ShopingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShopingCart>(userName, cancellationToken);
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShopingCart> StoreBasket(ShopingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }
        public async Task<bool> DeleteBaslet(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShopingCart>(userName, cancellationToken);
            if (basket is null)
                throw new BasketNotFoundException(userName);
            session.Delete<ShopingCart>(userName);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
