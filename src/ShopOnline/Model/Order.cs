using System;
namespace ShopOnline.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Cart Cart { get; set; }
        public Payment Payment { get; set; }
        public Delivery Delivery { get; set; }
        public int TotalPrice { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
        }

        public Order(int id, DateTime date, Cart cart, Payment payment, Delivery delivery, int totalPrice, Customer customer)
        {
            Id = id;
            Date = date;
            Cart = cart;
            Payment = payment;
            Delivery = delivery;
            TotalPrice = totalPrice;
            Customer = customer;
        }

    }
}
