using System;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class OrderDaoDB : IOrderDao
    {
        DateTime Time = new DateTime();
      
       
        public DataBaseConnectionService DataBaseConnectionService { get; private set; }
        public OrderDaoDB()
            {
                DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
            }
            
        public Payment GetPaymentMethod(int paymentId)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT id,name,cost FROM payment_methods WHERE id={paymentId}";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();
            Payment payment=null;

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                float price = reader.GetFloat(2);
                payment = new Payment(id, name, price);
            }
           
            return payment;
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
                float price = reader.GetFloat(2);
                delivery = new Delivery(id ,name, price);
            }

            return delivery;
        }

        public void ConfirmOrder(Customer customer, Payment payment, Delivery delivery, Cart cart)
        {
            NpgsqlConnection con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"INSERT INTO orders  
                (date, customer_id, cart_id, paymentmethod_id, deliveryoption_id)
                VALUES (now(), {customer.id}, {cart.Id}, {payment.Id}, {delivery.Id});";

            con.Open();

            using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }
    }
}
