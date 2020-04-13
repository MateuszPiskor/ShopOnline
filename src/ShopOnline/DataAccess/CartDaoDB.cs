using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class CartDaoDB
    {
    public DataBaseConnectionService DataBaseConnectionService { get; private set; }
    
        public CartDaoDB()
        {
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }

        public void CreateEmptyCart()
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"INSERT INTO carts (id,total_price) VALUES((SELECT MAX(id) from carts) +1,0)";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }


        //public Cart GetCurrentCart()
        //{
        //    using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
        //    string command = $@"SELECT MAX(id) from carts";

        //    con.Open();

        //    using var preparedCommand = new NpgsqlCommand(command, con);
        //    using var reader = preparedCommand.ExecuteReader();

        //    Cart cart;
        //    while (reader.Read())
        //    {
        //        cart = new Cart() { I }
        //    }
        //    return Cart;
        //}
    }
}
