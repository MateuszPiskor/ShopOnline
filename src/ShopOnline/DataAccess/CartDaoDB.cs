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
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "magda", "Lena1234", "onlineshop");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "agnieszkachruszczyksilva", "startthis", "shop_online_project");
        }


        public void CreateEmptyCart()
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"INSERT INTO carts (id,total_price) VALUES((SELECT MAX(id) from carts) +1,0)";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }

        public void UpdateTotalPrice()
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = @"UPDATE carts
                                    SET total_price = (SELECT SUM(subtotal)FROM cart_items
                                    WHERE cart_items.cart_id = (SELECT MAX(id) from carts))
  					           WHERE id = (SELECT MAX(id) from carts)";
            //string command = @"UPDATE carts
            //                SET total_price = (SELECT SUM(subtotal) FROM cart_items ci
            //                INNER JOIN carts c ON ci.cart_id = c.id
            //                    WHERE ci.cart_id = (SELECT MAX(id) from carts))
            //             WHERE id = (SELECT MAX(id) from carts)";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }

        public Cart GetCurrentCart()
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"SELECT id,total_price from carts
                                WHERE id=(SELECT max(id) from carts);";

            con.Open();

            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();

            Cart Cart=null;
            while (reader.Read())
            {
                Cart = new Cart(reader.GetInt32(0), reader.GetInt32(1));
            }
            return Cart;
        }
    }
}
