using System;
namespace ShopOnline.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public Customer(int id)
        {
            Id = id;
        }
    }
}
