using System;
using System.Collections.Generic;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class OrderDaoDB : IOrderDao
    {
        DataBaseConnectionService DataBaseConnectionService;

        public OrderDaoDB()
        {
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "agnieszkachruszczyksilva", "startthis", "shop_online_project");
        }


        public void CreateOrder(Customer customer, Cart cart, Payment payment, Delivery delivery, int totalPrice)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"INSERT INTO orders(customer_id, cart_id, paymentmethod_id, deliveryoption_id, total_price) VALUES(@customerId, @cartId, @paymentId, @deliveryId, @totalPrice);";
            connectionObj.Open();

            using var cmd = new NpgsqlCommand(sql, connectionObj);
            cmd.Parameters.AddWithValue("@customerId", customer.Id);
            cmd.Parameters.AddWithValue("@cart_id", cart.Id);
            cmd.Parameters.AddWithValue("@paymentId", payment.Id);
            cmd.Parameters.AddWithValue("@deliveryId", delivery.Id);
            cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
            cmd.ExecuteNonQuery();
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByCustomerId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrder()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder()
        {
            throw new NotImplementedException();
        }
    }
}
