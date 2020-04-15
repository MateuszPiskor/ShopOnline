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
        public CartDaoDB cartDaoDB=new CartDaoDB();
        public Cart Cart { get; set; }

        public ShopController(View view)
        {
            View = view;
            cartDaoDB.CreateEmptyCart();
            Cart=cartDaoDB.GetCurrentCart();
        }

        public void runShopController()
        {
            List<Product> allProducts = GetAllProducts();
            PrintProdutcs(allProducts);
            string userChoice = getUserChoice();
            HandleUserChoice(allProducts, userChoice);
        }

        private void HandleUserChoice(List<Product> allProducts, string userChoice)
        {

            if (int.Parse(userChoice) > 0 && int.Parse(userChoice) < allProducts.Count)
            {
                AddToBasket(userChoice,Cart.Id);
            }
        }

        public void AddToBasket(string userChoice,int cart_id)
        {
            var cartItemDaoDB = new CartItemDaoDB();
            cartItemDaoDB.AddToBasket(int.Parse(userChoice),cart_id);
            cartDaoDB.UpdateTotalPrice();
        }

        private string getUserChoice()
        {
            return View.GetAnswer();
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