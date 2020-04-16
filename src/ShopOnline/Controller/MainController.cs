using System;
using System.Collections.Generic;
using ShopOnline.Views;
using ShopOnline.Model;

namespace ShopOnline.Controller
{
    public class MainController
    { 
        View View=new View();
        bool isMainControllerActive = true; 
        Dictionary<string,string> request =new Dictionary<string, string>()
        {
            { "1", "Shop" }, 
            {"2", "Your account" },
            {"3", "Contact" },
            {"4","About us" },
            {"q", "Quit"}
        }; 
 
        
        public MainController()
        {
            
        }
        public void runMainController() {

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
                    case "3":
                        {
                            var contactController = new ContactController();
                            break;
                        }
                    case "4":
                        {
                            new ContactController().ShowAboutUsDetails("AboutUs");
                            break;
                        }
                    case "q":
                        {
                            isMainControllerActive = false;
                            break;
                        }
                }
            }
        }


    }
}
