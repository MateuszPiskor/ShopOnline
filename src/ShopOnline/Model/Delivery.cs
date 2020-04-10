using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Model
{
    public class Delivery
    {
        public string Name { get; set; }
        public float Cost { get; set; }

        public Delivery(string name, float cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}
