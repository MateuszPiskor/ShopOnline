using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class CartController
    {
        View view = new View();
        CartDaoDB cartDaoDB = new CartDaoDB();
        CartItemDaoDB cartItem=new CartItemDaoDB();
        int Cart_id;
        public CartController()
        {
            List<CartItem> cartItems= cartItem.GetCardItem();
            view.PrintCartItems(cartItems);
        }
        public CartController(int cart_id)
        {
            Cart_id = cart_id;
        }

    }
}
