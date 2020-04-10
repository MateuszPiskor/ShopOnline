using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Model
{
    public class Delivery
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }

        public Delivery(int id, string name, float cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
    }
}
