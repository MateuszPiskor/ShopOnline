using System;
namespace ShopOnline.DataAccess
{
    public interface ICartItemDao
    {
        void AddToBasket(int product_id, int quantity);
        void RemoveFromCart(int product_id, int quantity);
    }
}
