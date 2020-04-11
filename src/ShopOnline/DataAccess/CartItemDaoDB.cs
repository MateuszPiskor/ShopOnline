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
            DataBaseConnectionService=new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }

        public void CreateCart()
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string command = $"INSERT INTO carts (id) SELECT(SELECT MAX(id) from carts) +1";

            con.Open();
            using var preparedCommand = new NpgsqlCommand(command, con);
            using var reader = preparedCommand.ExecuteReader();
        }

        public void AddToBasket(int product_id,int quantity)
        {
            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            string productPrice = $"(SELECT price FROM products WHERE id = {product_id})";
            string command=$@"INSERT INTO cart_items(product_id, quantity, unit_price, subtotal)
                            VALUES({product_id}, {quantity},{productPrice} ,({quantity} * {productPrice}))";

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
            string command = $@"SELECT * FROM cart_items";

            con.Open();

            var preparedCommand=new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                CartItems.Add(new CartItem(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4)));
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
