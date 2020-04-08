using System;
using System.Collections.Generic;
using Npgsql;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public class ProductDaoDB : IProductDao
    {
        DataBaseConnectionService DataBaseConnectionService;

        public ProductDaoDB()
        {
            DataBaseConnectionService = new DataBaseConnectionService("localhost", "agnieszkachruszczyksilva", "startthis", "shop_online_project");
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            con.Open();

            string sql = @"SELECT movies.id, title, genres.name, production_year, director, description, rating
                           FROM movies
                           LEFT JOIN genres ON movies.genre_id = genres.id;";

            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Movie movie = new Movie(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetString(4), rdr.GetString(5), rdr.GetInt32(6));
                allMovies.Add(movie);
            }
            return allMovies;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();

            using var con = DataBaseConnectionService.GetDatabaseConnectionObject();
            con.Open();

            string sql = @"SELECT products.id, media_types.name, movies.id, title, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id; ";

            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Movie movie = new Movie(rdr.GetInt32(2),
                                        rdr.GetString(3), rdr.GetString(4), rdr.GetInt32(5), rdr.GetString(6), rdr.GetString(7), rdr.GetInt32(8));
                Product product = new Product(rdr.GetInt32(0), rdr.GetString(1), movie, rdr.GetInt32(9));
                allProducts.Add(product);
            }

            return allProducts;
        }
    }
}
