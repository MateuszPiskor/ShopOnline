using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class ShopController
    {
        public View view = new View();
        public CartDaoDB cartDaoDB=new CartDaoDB();
        Validation validation = new Validation();
        public Cart Cart { get; set; }
        bool isShopControllerActive = true;


        Dictionary<string, string> request = new Dictionary<string, string>()
        {
           
            {"1", "Add to basket" },
            {"2", "Edit basket" },
            {"3", "Go to checkout" },
            {"4","Back to filters" },

        };
        public ShopController()
        {
            Cart = new Cart();

        }

        public void runShopController()
        {
            while (isShopControllerActive)
            {
                Cart = cartDaoDB.GetCurrentCart();
                view.PrintDictionary(request);
                string userChoice = view.GetUserInput("Your Choice: ");
                switch (userChoice)
                {
                    
                    case "1":
                        string product_id = view.GetUserInput("Type product id: ");
                        if (validation.isProductNumber(product_id))
                        {
                            AddToBasket(product_id, Cart.Id);
                            //Cart = cartDaoDB.GetCurrentCart();
                            view.PrintMessage("Item added");
                        }
                        break;

                    case "2":
                        
                        if (Cart.TotalPrice > 0)
                        {
                            var cartController = new CartController(Cart);
                            cartController.runCartController();
                        }
                        else
                        {
                            view.PrintMessage("Your basket is empty");
                        }
                            
                        break;

                    case "3":
                        if (Cart.TotalPrice > 0)
                        {
                            var orderController = new OrderController(Cart);
                            orderController.RunOrderController();
                            if (orderController.IsActive == false)
                            {
                                isShopControllerActive = false;
                            }
                        }
                        else
                        {
                            view.PrintMessage("Your cart is empty. Buy something:)");
                        }
                        break;

                    case "4":
                        isShopControllerActive = false;
                        break;

                    default:
                        view.PrintMessage("Choose one of the available options");
                        break;
                }
                
            }
        }

        public void AddToBasket(string userChoice,int cart_id)
        {
            var cartItemDaoDB = new CartItemDaoDB();
            cartItemDaoDB.AddToBasket(int.Parse(userChoice),cart_id);
            cartDaoDB.UpdateTotalPrice();
            Cart = cartDaoDB.GetCurrentCart();
        }

        private List<Product> GetAllProducts()
        {
            ProductDaoDB productDaoDB = new ProductDaoDB();
            var allProducts = productDaoDB.GetAllProducts();
            return allProducts;
        }

        private void PrintProdutcs(List<Product> allProducts)
        {
            view.PrintProducts(allProducts);
        }

    }
}