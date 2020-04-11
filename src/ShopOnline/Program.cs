using System;
using System.Collections.Generic;
using ShopOnline.Controller;
using ShopOnline.DataAccess;
using ShopOnline.Model;

namespace ShopOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            var cartItem = new CartItemDaoDB();
            List<CartItem> cartsItems = cartItem.GetCardItem();
            foreach(var item in cartsItems)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
