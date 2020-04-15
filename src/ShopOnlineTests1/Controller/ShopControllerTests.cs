using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopOnline.Controller;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Controller.Tests
{
    [TestClass()]
    public class ShopControllerTests
    {
        [TestMethod()]
        public void AddToBasketTest()
        {
            var item=new CartItemDaoDB();
            item.AddToBasket(44, 19);
        }
    }
}