namespace Basket.API.Models
{
    public class ShopingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
        public ShopingCart(string userName)
        {
            UserName = userName;
        }

        //Required for Mapping
        public ShopingCart()
        {
            
        }
    }
}
