using System;
namespace ShopOnline.Model
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public Delivery()
        {
        }

        public Delivery(int id, string name, double cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
    }
}
