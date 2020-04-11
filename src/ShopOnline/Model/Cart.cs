using System;
namespace ShopOnline.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public Cart(int id)
        {
            Id = id;
        }
    }
}
