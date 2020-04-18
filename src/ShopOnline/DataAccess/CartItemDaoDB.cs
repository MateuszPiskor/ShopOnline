using System;
using System.Collections.Generic;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class CartItemDaoDB : ICartItemDao
    {
        public DataBaseConnectionService DataBaseConnectionService { get; private set; }

        public CartItemDaoDB()
        {
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "magda", "Lena1234", "onlineshop");
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres","1234","ShopOnline");
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }

        public void AddToBasket(int product_id, int cart_id)
        {
            int defaultQuantity = 1;
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string productPrice = $"(SELECT price FROM products WHERE id = {product_id})";
            string command=$@"INSERT INTO cart_items(product_id, quantity, unit_price, subtotal,cart_id)
                            VALUES({product_id}, {defaultQuantity},{productPrice} ,({defaultQuantity}* {productPrice}),{cart_id})";

            con.Open();

            using var preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }

        public void ChangeQuantity(int product_id, int quantity)
        {
            NpgsqlConnection con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command=$@"UPDATE cart_items
                SET quantity = quantity + {quantity},subtotal=(quantity+{quantity})*unit_price
                WHERE product_id = {product_id}";

            con.Open();

            using var preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();
        }

        public void RemoveWhenIsZero()
        {
            NpgsqlConnection con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"DELETE FROM cart_items
                                        WHERE quantity<= 0";

            con.Open();

            using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();

        }

        public List<CartItem> GetCardItem()
        {
            List<CartItem> CartItems = new List<CartItem>();
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $@"SELECT m.title,c.id,c.product_id,c.quantity,c.unit_price,c.subtotal 
                                    FROM cart_items c
                                    INNER JOIN products p
                                    ON c.product_id=p.id
                                     INNER JOIN movies m
                                     ON m.id=p.movie_id
                                     WHERE c.cart_id=(SELECT MAX(id) from carts)";

            con.Open();

            var preparedCommand=new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                CartItems.Add(new CartItem(reader.GetString(0),reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5)));
            }

            return CartItems;
        }
        public void ClearCart()
        {
            NpgsqlConnection con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = "DELETE FROM cart_items";

            con.Open();

            using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
            preparedCommand.ExecuteNonQuery();

        }
    }
}
