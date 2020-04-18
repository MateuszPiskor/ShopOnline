using System;
using System.Collections.Generic;
using ShopOnline.Views;
using ShopOnline.Model;
using System.Linq;
using ShopOnline.DataAccess;

namespace ShopOnline.Controller
{
    public class MainController
    { 
        View view=new View();
        bool isMainControllerActive = true; 
        Dictionary<int,string> requests =new Dictionary<int, string>()
        {
            {1, "Shop" }, 
            {2, "Contact" },
            {3, "About us" },
            {4, "Quit"}
        }; 
 
        
        public MainController()
        {
            
        }
        public void runMainController() {

            while (isMainControllerActive)
            {
                view.PrintDictionary(requests);
                int mainMenuChoice = GetAnOption();

                switch (mainMenuChoice)
                {
                    case 1: 
                        {
                            
                            var cartDaoDB = new CartDaoDB();
                            cartDaoDB.CreateEmptyCart();
                            var productController = new ProductController();
                            productController.RunProductController();
                            break;
                        }
                    case 2:
                        {
                            var contactController = new ContactController();
                            break;
                        }
                    case 3:
                        {
                            new AboutUs().ShowAboutUsDetails("AboutUs");
                            break;
                        }
                    case 4:
                        {
                            isMainControllerActive = false;
                            break;
                        }
                }
            }
        }

        private int GetAnOption()
        {
            while (true)
            {
                string input = view.GetUserInput("Choose option: ");

                if (Int32.TryParse(input, out int number) && requests.ContainsKey(Int32.Parse(input)))
                {
                    int output = Int32.Parse(input);
                    return output;
                }
                else
                {
                    view.PrintMessage($"Please enter a number between {requests.Keys.Min()} and {requests.Keys.Max()}");
                }

            }
        }


    }
}
