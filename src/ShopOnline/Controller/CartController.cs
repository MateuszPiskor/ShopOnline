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
        CartItemDaoDB cartItemDaoDB = new CartItemDaoDB();
        Cart Cart { get; set; }
        bool isCartControllerActive = true;

        Dictionary<string, string> optionsInBasket = new Dictionary<string, string>()
        {
            {"1", "Add" },
            {"2", "Remove" },
            {"3", "Back To Shoping" },
            {"4","Go to checkout" }

        };

        public string userChoice { get; set; }

        public CartController(Cart cart)
        {
            Cart = cart;
        }

        public void runCartController()
        {
            while (isCartControllerActive)
            {
                List<CartItem> cartItems = cartItemDaoDB.GetCardItem();
                Cart = cartDaoDB.GetCurrentCart();
                view.PrintBasket(cartItems, Cart);
                view.PrintDictionary(optionsInBasket);
                userChoice = view.GetUserInput("Your choice: ");

                switch (userChoice)
                {
                    case "1":

                        EditBasket(cartItems, userChoice);
                        break;
                    
                    case "2":
                        EditBasket(cartItems, userChoice);
                        break;

                    case "3":
                        isCartControllerActive = false;
                        
                        break;
                    
                    case "4":
                        if (Cart.TotalPrice > 0)
                        {
                            var orderController = new OrderController(Cart);
                            orderController.RunOrderController();
                            if (orderController.IsActive == false)
                            {
                                isCartControllerActive = false;
                            }
                        }
                        else
                        {
                            view.PrintMessage("Your cart is empty. Buy something:)");
                        }
                        break;

                    default:
                        view.PrintMessage("Choose one of the available options");
                        break;
                }
            }
        }

        

        private void EditBasket(List<CartItem> cartItems,string basketOperation)
        {
            int product_id = int.Parse(view.GetUserInput("Type product id: "));
   
            int quantity = int.Parse(view.GetUserInput("How many: "));
            if (basketOperation == "1" || basketOperation == "2")
            {
                if (basketOperation == "2")
                {
                    quantity *= -1;
                }
                cartItemDaoDB.ChangeQuantity(product_id, quantity); 
                cartDaoDB.UpdateTotalPrice();
                cartItemDaoDB.RemoveWhenIsZero();
            }
        }
    }
}


