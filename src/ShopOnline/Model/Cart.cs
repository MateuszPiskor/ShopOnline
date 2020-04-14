using System;
using System.Collections.Generic;

namespace ShopOnline.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public double  TotalPrice { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart() { }

        public Cart(int id, double totalPrice)
        {
            Id = id;
            TotalPrice = totalPrice;
        }
    }
}
