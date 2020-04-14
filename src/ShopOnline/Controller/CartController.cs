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
        Cart Cart { get; set; }
        public string userChoice { get; set; }

        public CartController(Cart cart)
        {
            Cart = cart;
            runCartController();
        }

        private void runCartController()
        {
            List<CartItem> cartItems = cartItem.GetCardItem();
            view.PrintCartItems(cartItems);
            view.PrintMessage($"Total : {Cart.TotalPrice}");
            //userChoice=view.GetUserInput($"Legend: \nTo edit basket-- > press 'e'\nContinue shopping-- > press 's'\nGo to checkout-- > press 'c'");
            //handleUserChoice(userChoice);
        }

        private void handleUserChoice(string userChoice)
        {
            if(userChoice=="s")
            {
                
            }
        }
    }
}
