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
            HandleUserChoice();
        }

        private void HandleUserChoice()
        {
            Validation validation = new Validation();
            bool correct = false;
            string userChoice = "";
            while (!correct)
            {
                userChoice=View.GetUserInput("What do you chosse? : ");
                if (validation.isProductNumber(userChoice))
                {
                    AddToBasket(userChoice, Cart.Id);
                }

                else if(userChoice=="c" && userChoice == "C")
                {
                    var cartController=new CartController();
                }
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