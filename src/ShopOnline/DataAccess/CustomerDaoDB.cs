using System.Collections.Generic;
using ShopOnline.Model;
using Npgsql;
using System;



namespace ShopOnline.DataAccess
{
    public class CustomerDaoDB : ICustomerDao
    {
        DataBaseConnectionService DataBaseConnectionService;

        public CustomerDaoDB()
        {
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "magda", "Lena1234", "onlineshop");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "agnieszkachruszczyksilva", "startthis", "shop_online_project");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomers = new List<Customer>();

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = "SELECT * FROM customers";
            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                allCustomers.Add(ParseDBTo(rdr));

            }
            return allCustomers;

        }

        public Customer GetCustomerById(int id)
        {

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $"SELECT * FROM customers WHERE id = {id}";
            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            Customer customer;
            if (rdr.Read())
            {
                customer = ParseDBTo(rdr);
            } else
            {
                throw new IdNotFoundException("Customer ID not found in customers table");
            }

            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $"SELECT * FROM customers WHERE email LIKE '%{email}'";
            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            Customer customer;
            if (rdr.Read())
            {
                customer = ParseDBTo(rdr);
            }
            else
            {
                throw new IdNotFoundException("Customer email not found in customers table");
            }

            return customer;
        }


        
        public void RemoveCustomer(int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();

            var sql = $"DELETE FROM customers WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);


            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void UpdateCustomerDetails(string newCity, string newPostCode, string newStreet, string newEmail, string newPhone, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET city='{newCity}', post_code='{newPostCode}'" +
                      $",street='{newStreet}', email='{newEmail}', phone='{newPhone}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

            cmd.Parameters.AddWithValue("city", newCity);
            cmd.Parameters.AddWithValue("post_code", newPostCode);
            cmd.Parameters.AddWithValue("street", newStreet);
            cmd.Parameters.AddWithValue("email", newEmail);
            cmd.Parameters.AddWithValue("phone", newPhone);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public void InsertNewCustomer(string firstName, string lastName, string city,
                                      string postCode, string street, string email, string phone,
                                      bool isRegistered, string password)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = "INSERT INTO customers(first_name, last_name, city, post_code, street, email, phone, is_registered, password) "
                       + "VALUES (@first_name, @last_name, @city, @post_code, @street, @email, @phone, @is_registered, @password)";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

            cmd.Parameters.AddWithValue("first_name", firstName);
            cmd.Parameters.AddWithValue("last_name", lastName);
            cmd.Parameters.AddWithValue("city", city);
            cmd.Parameters.AddWithValue("post_code", postCode);
            cmd.Parameters.AddWithValue("street", street);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("phone", phone);
            cmd.Parameters.AddWithValue("is_registered", isRegistered);
            cmd.Parameters.AddWithValue("password", password);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public void UpdateCustomerPhoneNumber(string newPhone, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET  phone='{newPhone}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

           
            cmd.Parameters.AddWithValue("phone", newPhone);


            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public void UpdateCustomerEmail(string newEmail, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET email='{newEmail}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);


            cmd.Parameters.AddWithValue("email", newEmail);
           
            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public void UpdateCustomerStreet(string newStreet, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET street='{newStreet}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

      
            cmd.Parameters.AddWithValue("street", newStreet);
           
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void UpdateCustomerPosteCode(string newPostCode, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET post_code='{newPostCode}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

       
            cmd.Parameters.AddWithValue("post_code", newPostCode);
           
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void UpdateCustomerCity(string newCity, int id)
        {
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            connectionObj.Open();
            var sql = $"UPDATE customers SET city='{newCity}' WHERE id = {id}";
            using var cmd = new NpgsqlCommand(sql, connectionObj);

            cmd.Parameters.AddWithValue("city", newCity);
          
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        private Customer ParseDBTo(NpgsqlDataReader rdr)
        {

            CustomerDetails details = new CustomerDetails(rdr.GetString(3), rdr.GetString(4),
                                                              rdr.GetString(5), rdr.GetString(6),
                                                              rdr.GetString(7));
            
            Customer customer = new Customer(rdr.GetInt32(0),
                                             rdr.GetString(1),
                                             rdr.GetString(2),
                                             details,
                                             rdr.GetBoolean(8),
                                             rdr.GetString(9));
         
            

            return customer;


        }

    }
}