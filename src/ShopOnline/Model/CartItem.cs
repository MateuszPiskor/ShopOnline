using System;
namespace ShopOnline.Model
{
    public class CartItem
    {
        public int ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public int Unit_price { get; set; }
        public int Subtotal { get; set; }
        public int Card_id { get; set; }

        public CartItem()
        {
            
        }
    }
}
