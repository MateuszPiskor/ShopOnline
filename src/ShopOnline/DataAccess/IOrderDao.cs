using System;
using System.Collections.Generic;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public interface IOrderDao
    {
        void CreateOrder(Customer customer, Cart cart, Payment payment, Delivery delivery, int totalPrice);
        void CreateOrder(Order order);
        Payment GetPaymentMethod(int paymentId);
        List<Payment> GetAllPaymentMethods();
        Delivery GetDeliveryOption(int deliveryId);
        void RemoveOrder();
        void UpdateOrder();
        Order GetOrderById(int id);
        List<Order> GetOrdersByCustomerId(int id);
        List<Order> GetOrdersByDate(DateTime date);

    }
}
