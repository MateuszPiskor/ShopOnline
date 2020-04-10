using System;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class OrderDaoDB : IOrderDao
    {
        public DataBaseConnectionService DataBaseConnectionService { get; private set; }
        public OrderDaoDB()
            {
                DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
            }
            
        public Payment GetPaymentMethod(int id)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT name,cost FROM payment_methods WHERE id={id}";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();
            Payment payment=null;

            while (reader.Read())
            {
                string name = reader.GetString(0);
                float price = reader.GetFloat(1);
                payment = new Payment(name, price);
            }
           
            return payment;
        }

        public Delivery GetDeliveryOption(int id)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"SELECT name,cost FROM delivery_options WHERE id={id}";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();
            Delivery delivery = null;

            while (reader.Read())
            {
                string name = reader.GetString(0);
                float price = reader.GetFloat(1);
                delivery = new Delivery(name, price);
            }

            return delivery;
        }

        //public Order PlaceOrder()
        //{

        //}
    }
}
