using System;
namespace ShopOnline.DataAccess
{
    public interface ICartItemDao
    {
        void AddToBasket(int product_id, int quantity);
        void ChangeQuantity(int product_id, int quantity);
    }
}
