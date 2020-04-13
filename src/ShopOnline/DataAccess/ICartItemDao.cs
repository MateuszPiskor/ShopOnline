using System;
using System.Collections.Generic;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public interface ICartItemDao
    {
        void AddToBasket(int product_id, int cart_id);
        void ChangeQuantity(int product_id, int quantity);
        List<CartItem> GetCardItem();
        void RemoveWhenIsZero();
        void ClearCart();
    }
}
