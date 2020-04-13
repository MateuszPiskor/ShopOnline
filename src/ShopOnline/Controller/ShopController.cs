using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class ShopController
    {
        public View View { get; private set; }
        public CartDaoDB CartDB { get; set; }

        public ShopController(View view)
        {
            View = view;
            CartDB = new CartDaoDB();
        }

        public void runShopController()
        {
            CartDB.CreateEmptyCart();
            List<Product> allProducts = GetAllProducts();
            PrintProdutcs(allProducts);
            string userChoice = getUserChoice();
            HandleUserChoice(allProducts, userChoice);
        }

        private void HandleUserChoice(List<Product> allProducts, string userChoice)
        {
            if (int.Parse(userChoice) > 0 && int.Parse(userChoice) < allProducts.Count)
            {
                //var Cart = CartDB.GetCurrentCart();
                AddToBasket(userChoice);
            }
        }

        private static void AddToBasket(string userChoice)
        {
            var cartItemDaoDB = new CartItemDaoDB();
            cartItemDaoDB.AddToBasket(int.Parse(userChoice));
        }

        private string getUserChoice()
        {
            return View.getAnswer();
        }

        private List<Product> GetAllProducts()
        {
            ProductDaoDB productDaoDB = new ProductDaoDB();
            var allProducts = productDaoDB.GetAllProducts();
            return allProducts;
        }

        private void PrintProdutcs(List<Product> allProducts)
        {
            View.PrintProducts(allProducts);
        }

    }
}