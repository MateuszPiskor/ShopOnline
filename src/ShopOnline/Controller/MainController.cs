using System;
using System.Collections.Generic;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class MainController
    { 
        View View=new View();
        Dictionary<string,string> request =new Dictionary<string, string>()
        {
            { "1", "Shop" }, 
            {"2", "Your account" },
            {"3", "Contact" },
            {"4","About us" }
        }; 
 
        
        public MainController()
        {
            
        }
        public void runMainController() {
            bool isMainControllerActive = true;

            while (isMainControllerActive)
            {
                View.PrintDictionary(request);
                string mainMenuChoice=View.GetUserInput("Your Choice: ");

                switch (mainMenuChoice)
                {
                    case "1": 
                        {
                            var productController = new ProductController();
                            productController.RunProductController();
                            break;
                        }
                }
            }
        }


    }
}
