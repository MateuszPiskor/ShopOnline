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
            {"2", "Go to basket" },
            {"3", "Go to checkout" },
            {"4","Go to filters" },

        };

        public void runShopController()
        {
            cartDaoDB.CreateEmptyCart();

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
                            Cart = cartDaoDB.GetCurrentCart();
                            view.PrintMessage("Item added");
                        }
                        break;

                    case "2":
                            var cartController = new CartController(Cart);
                            cartController.runCartController();
                        break;

                    case "3":
                        break;

                    case "4":
                        isShopControllerActive = false;
                        break;
                }
                
            }
        }

        public void AddToBasket(string userChoice,int cart_id)
        {
            var cartItemDaoDB = new CartItemDaoDB();
            cartItemDaoDB.AddToBasket(int.Parse(userChoice),cart_id);
            cartDaoDB.UpdateTotalPrice();
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