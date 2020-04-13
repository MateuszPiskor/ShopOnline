using System;
using System.Collections.Generic;
using ShopOnline.Controller;
using ShopOnline.DataAccess;
using ShopOnline.Model;

namespace ShopOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            var shopController=new MainController();
            shopController.runMainController();
        }
    }
}
