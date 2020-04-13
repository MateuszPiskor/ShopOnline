using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopOnline.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.DataAccess.Tests
{
    [TestClass()]
    public class CartDaoDBTests
    {
        [TestMethod()]
        public void CreateEmptyCartTest()
        {
            var cart=new CartDaoDB();
            cart.CreateEmptyCart();

        }
    }
}