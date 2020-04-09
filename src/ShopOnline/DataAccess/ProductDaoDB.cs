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

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"SELECT movies.id, title, genres.name, production_year, director, description, rating
                           FROM movies
                           LEFT JOIN genres ON movies.genre_id = genres.id;";

            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ParseDBTo(allMovies, rdr);
            }
            return allMovies;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"SELECT products.id, media_types.name, movies.id, title, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id; ";

            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ParseDBTo(allProducts, rdr);
            }

            return allProducts;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT products.id, media_types.name, movies.id, title, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE products.id = {id};";

            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sql, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                product = ParseDBTo(product, rdr);
            }

            return product;
        }

        private void ParseDBTo(List<Movie> allMovies, NpgsqlDataReader rdr)
        {
            string genre = rdr.GetString(2);
            Genre parsedGenre = (Genre)Enum.Parse(typeof(Genre), genre);
            allMovies.Add(new Movie(rdr.GetInt32(0), rdr.GetString(1), parsedGenre, rdr.GetInt32(3), rdr.GetString(4), rdr.GetString(5), rdr.GetInt32(6)));
        }

        private void ParseDBTo(List<Product> allProducts, NpgsqlDataReader rdr)
        {
            Product product = new Product();
            product = ParseDBTo(product, rdr);
            allProducts.Add(product);
        }

        private Product ParseDBTo(Product product, NpgsqlDataReader rdr)
        {
            string genre = rdr.GetString(4);
            Genre parsedGenre = (Genre)Enum.Parse(typeof(Genre), genre);
            Movie movie = new Movie(rdr.GetInt32(2), rdr.GetString(3), parsedGenre, rdr.GetInt32(5), rdr.GetString(6), rdr.GetString(7), rdr.GetInt32(8));
            string mediaType = rdr.GetString(1);
            MediaType parsedMediaType = (MediaType)Enum.Parse(typeof(MediaType), mediaType);

            product.Id = rdr.GetInt32(0);
            product.MediaType = parsedMediaType;
            product.Movie = movie;
            product.Price = rdr.GetInt32(9);
            return product;
        }

    }
}
