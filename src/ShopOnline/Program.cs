using System;
using ShopOnline.DataAccess;
using ShopOnline.Views;
using ShopOnline.Model;
using System.Collections.Generic;
using ShopOnline.Controller;

namespace ShopOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainController=new MainController();
            mainController.runMainController();
            
        }
    }
}
