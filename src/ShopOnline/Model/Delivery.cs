using System;
using System.Collections.Generic;
using System.Text;

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

        public override string ToString()
        {
            return $"{Id} - {Name} - price: {Cost} zł";
        }
    }
}
