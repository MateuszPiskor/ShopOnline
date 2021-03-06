using System;
using System.Collections.Generic;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class OrderDaoDB : IOrderDao
    {
        public DataBaseConnectionService DataBaseConnectionService { get; private set; }

        public OrderDaoDB()
        {
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "magda", "Lena1234", "onlineshop");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres","1234","ShopOnline");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }

        public void CreateOrder(Customer customer, Cart cart, Payment payment, Delivery delivery, double totalPrice)
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

        public void CreateOrder(Order order)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"INSERT INTO orders(date, customer_id, cart_id, paymentmethod_id, deliveryoption_id, total_price) VALUES(CURRENT_DATE, @customerId, @cartId, @paymentId, @deliveryId, @totalPrice);";
            connectionObj.Open();

            using var cmd = new NpgsqlCommand(sql, connectionObj);
            cmd.Parameters.AddWithValue("@customerId", order.Customer.Id);
            cmd.Parameters.AddWithValue("@cartId", order.Cart.Id);
            cmd.Parameters.AddWithValue("@paymentId", order.Payment.Id);
            cmd.Parameters.AddWithValue("@deliveryId", order.Delivery.Id);
            cmd.Parameters.AddWithValue("@totalPrice", order.TotalPrice);
            cmd.ExecuteNonQuery();
        }

        public Payment GetPaymentMethod(int paymentId)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT id,name,cost FROM payment_methods WHERE id={paymentId}";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();

            Payment payment = null;

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                double price = reader.GetDouble(2);
                payment = new Payment(id, name, price);
            }
            return payment;
        }

        public List<Payment> GetAllPaymentMethods()
        {
            List<Payment> allPayments = new List<Payment>();

            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT id,name,cost FROM payment_methods;";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                double price = reader.GetDouble(2);
                allPayments.Add(new Payment(id, name, price));
            }
            return allPayments;
        }

        public List<Delivery> GetAllDeliveryOptions()
        {
            List<Delivery> allDeliveries = new List<Delivery>();

            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT id,name,cost FROM delivery_options;";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                double price = reader.GetDouble(2);
                allDeliveries.Add(new Delivery(id, name, price));
            }
            return allDeliveries;
        }

        public Delivery GetDeliveryOption(int deliveryId)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT id,name,cost FROM delivery_options WHERE id={deliveryId}";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();
            Delivery delivery = null;

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                double price = reader.GetDouble(2);
                delivery = new Delivery(id, name, price);
            }

            return delivery;
        }

        public void ConfirmOrder(Customer customer, Payment payment, Delivery delivery, Cart cart)
        {
            NpgsqlConnection con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"INSERT INTO orders  
                (date, customer_id, cart_id, paymentmethod_id, deliveryoption_id)
                VALUES (now(), {customer.Id}, {cart.Id}, {payment.Id}, {delivery.Id});";

            con.Open();

            using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
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

        public List<CartItem> GetCartItemsOfLastCart()
        {
            List<CartItem> cartItems = new List<CartItem>();

            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"SELECT m.title,c.id,c.product_id,c.quantity,c.unit_price,c.subtotal 
                                    FROM cart_items c
                                    INNER JOIN products p
                                    ON c.product_id=p.id
                                     INNER JOIN movies m
                                     ON m.id=p.movie_id
                                     WHERE c.cart_id=(SELECT MAX(id) from carts)";

            con.Open();

            var preparedCommand = new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                cartItems.Add(new CartItem(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
            }

            return cartItems;
        }

        public DateTime GetLastOrderDate()
        {
            DateTime date = new DateTime();
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"SELECT * FROM orders
                                     WHERE id=(SELECT MAX(id) from orders)";

            con.Open();

            var preparedCommand = new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                date = reader.GetDateTime(1);
            }

            return date;
        }

        public int GetLastOrderId()
        {
            int id = 0;
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"SELECT * FROM orders
                                     WHERE id=(SELECT MAX(id) from orders)";

            con.Open();

            var preparedCommand = new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32(0);
            }

            return id;
        }
    }
}
