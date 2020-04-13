using System;
namespace ShopOnline.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public Cart(int id, double totalPrice)
        {
            Id = id;
            TotalPrice = totalPrice;
        }

        

    }
}
