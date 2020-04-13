using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOnline.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public Payment()
        {
        }

        public Payment(int id, string name, double cost)

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
