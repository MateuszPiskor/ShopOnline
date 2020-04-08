using System;
using System.Collections.Generic;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class CartItemDaoDB : ICartItemDao
    {
        string cs = "Host=localhost;Username=postgres;Password=1234;Database=ShopOnline";
        public CartItemDaoDB()
        {
        }
        public void CreateCart()
        {
            string sql = $"INSERT INTO carts (id) SELECT(SELECT MAX(id) from carts) +1";
            HandleSqlQuery(sql);
        }

        public void AddToBasket(int product_id,int quantity)
        {
            string productPrice = $"(SELECT price FROM products WHERE id = {product_id})";
            string sql=$@"INSERT INTO cart_items(product_id, quantity, unit_price, subtotal)
                            VALUES({product_id}, {quantity},{productPrice} ,({quantity} * {productPrice}))";
            HandleSqlQuery(sql);
        }
        public void RemoveFromCart(int product_id, int quantity)
        {
            string sql=$@"UPDATE cart_items
                SET quantity = quantity - {quantity}
                WHERE id = 46";
            HandleSqlQuery(sql);
            string sqlCheckQuantity= $@"DELETE FROM cart_items
                                        WHERE quantity<= 0";
            HandleSqlQuery(sql);
        }

        public List<CartItem> GetCardItem()
        {
            List<CartItem> CartItems = new List<CartItem>();
            var con=GetConnectionDatabaseObject();
            string command = $@"SELECT * FROM cart_items";

            con.Open();

            var preparedCommand=new NpgsqlCommand(command, con);
            var reader = preparedCommand.ExecuteReader();

            while (reader.Read())
            {
                CartItems.Add(new CartItem()
                {
                   ID=reader.GetInt32(0),
                   Product_ID = reader.GetInt32(1),
                   Quantity=reader.GetInt32(2),
                   Unit_price=reader.GetInt32(3),
                   Subtotal=reader.GetInt32(4),
                   Card_id=reader.GetInt32(5)
                });
            }

            return CartItems;
        }

        public NpgsqlConnection GetConnectionDatabaseObject()
        {
            using var con = new NpgsqlConnection(cs);
            return con;
        }

        private void HandleSqlQuery(string sql)
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
        }

    }
}
