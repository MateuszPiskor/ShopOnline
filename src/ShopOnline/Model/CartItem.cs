using System;
namespace ShopOnline.Model
{
    public class CartItem
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int Subtotal { get; set; }

        public CartItem(int id, int product_id, int quantity, int unitPrice, int subtotal)
        {
            ID = id;
            Product_ID = product_id;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Subtotal = subtotal;

        }
        public override string ToString()
        {
            return $"{ID} {Product_ID} {Quantity} {UnitPrice} {Subtotal}";
        }
    }
}
