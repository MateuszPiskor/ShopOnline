using System;
namespace ShopOnline.Model
{
    public class CartItem
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Subtotal { get; set; }

        public string Title { get; set; }

        public CartItem(string title,int id, int product_id, int quantity, double unitPrice, double subtotal)
        {
            ID = id;
            Product_ID = product_id;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = subtotal;
            Title = title;

        }
        public override string ToString()
        {
            return $"{Product_ID}.{Title} item:{Quantity} price:{UnitPrice} subtotal:{Subtotal}";
        }
    }
}
