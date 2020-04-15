using System;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class MainController
    {
        public string[] menu = { "1. Shop", "2. Your account","3. Contact","4. About us" };
        public View View { get; set; }
        public MainController()
        {
            View=new View(); 
        }
        public void runMainController() {
            View.DisplayMenu(menu);
            string mainMenuChoice=View.GetAnswer();
            switch (mainMenuChoice)
            {
                case "1": 
                    {
                        var shopController = new ShopController(View);
                        shopController.runShopController();
                        break;
                    }
            }
        }


    }
}
