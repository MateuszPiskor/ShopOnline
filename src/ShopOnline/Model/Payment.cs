using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Model
{
    public class Payment
    {
        public string Name { get; set; }
        public float Cost { get; set; }

        public Payment(string name, float cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}
