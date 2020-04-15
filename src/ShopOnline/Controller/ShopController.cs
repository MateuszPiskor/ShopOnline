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
            {"1","Filter products" },
            {"2", "Add to basket" },
            {"3", "Go to basket" },
            {"4", "Go to checkout" },
            {"5","Go main menu" },

        };

        public void runShopController()
        {
            cartDaoDB.CreateEmptyCart();

            while (isShopControllerActive)
            {
                Cart = cartDaoDB.GetCurrentCart();
                List<Product> allProducts = GetAllProducts();
                PrintProdutcs(allProducts);
                view.PrintDictionary(request);
                string userChoice = view.GetUserInput("Your Choice: ");
                switch (userChoice)
                {
                    case "1":
                        var productController=new ProductController();
                        productController.RunProductController();
                        break;
                    case "2":
                        string product_id = view.GetUserInput("Type product id: ");
                        if (validation.isProductNumber(product_id))
                        {
                            AddToBasket(userChoice, Cart.Id);
                            Cart = cartDaoDB.GetCurrentCart();
                            view.PrintMessage("Item added");
                        }
                        break;

                    case "3":
                            var cartController = new CartController(Cart);
                            cartController.runCartController();
                        break;

                    case "4":
                        break;

                    case "5":
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