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
            //DataBaseConnectionService = new DataBaseConnectionService("localhost", "postgres", "1234", "ShopOnline");
        }


        public List<Movie> GetAllMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"SELECT movies.id, title, genres.id, genres.name, production_year, director, description, rating
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

        public Product GetProductById(int id)
        {
            Product product = new Product();
            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
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

        public List<Product> GetAllProducts()
        {
            string sqlQuery = @"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id; ";

            return GetSelectedProducts(sqlQuery);
        }

        public List<Product> GetProductsByTitlePart(string partOfTitle)
        {
            string sqlQuery = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE title LIKE '%{partOfTitle}%' OR title LIKE '%{partOfTitle.ToUpper()}%'; ";

            return GetSelectedProducts(sqlQuery);
        }

        public List<Product> GetProductsByGenre(int genreId)
        {
            string sqlQuery = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE genres.id = '{genreId}'; ";

            return GetSelectedProducts(sqlQuery);
        }

        public Dictionary<int, string> GetAllGenres()
        {
            string sqlQuery = @$"SELECT DISTINCT genres.id, genres.name
                           FROM products
                           INNER JOIN movies ON products.movie_id = movies.id
                           INNER JOIN genres ON movies.genre_id = genres.id;";

            return GetSelectedTypes(sqlQuery);
        }

        public List<Product> GetProductsByMediaType(int mediaTypeId)
        {
            string sqlQuery = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE media_types.id = '{mediaTypeId}'; ";

            return GetSelectedProducts(sqlQuery);
        }

        public Dictionary<int, string> GetAllMediaTypes()
        {
            string sqlQuery = @$"SELECT DISTINCT media_types.id, media_types.name
                                 FROM products
                                 INNER JOIN media_types ON products.mediatype_id = media_types.id; ";

            return GetSelectedTypes(sqlQuery);
        }

        public List<Product> GetProductsByDirector(string director)
        {
            string sqlQuery = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE director = '{director}'; ";

            return GetSelectedProducts(sqlQuery);
        }

        public List<Product> GetProductsByRating(int rating)
        {
            string sqlQuery = @$"SELECT products.id, media_types.id, media_types.name, movies.id, title, genres.id, genres.name, production_year, director, description, rating, price
                           FROM products
                           LEFT JOIN media_types ON products.mediatype_id = media_types.id
                           LEFT JOIN movies ON products.movie_id = movies.id
                           LEFT JOIN genres ON movies.genre_id = genres.id
                           WHERE rating = '{rating}'; ";

            return GetSelectedProducts(sqlQuery);
        }

        private List<Product> GetSelectedProducts(string sqlQuery)
        {
            List<Product> allProducts = new List<Product>();

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = sqlQuery;

            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ParseDBTo(allProducts, rdr);
            }

            return allProducts;
        }

        private Dictionary<int, string> GetSelectedTypes(string sqlQuery)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            using var connectionObj = DataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = sqlQuery;

            connectionObj.Open();
            using var cmd = new NpgsqlCommand(sqlQuery, connectionObj);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                result.Add(rdr.GetInt32(0), rdr.GetString(1));
            }
            return result;
        }

        private void ParseDBTo(List<Movie> allMovies, NpgsqlDataReader rdr)
        {
            Genre genre = new Genre(rdr.GetInt32(2), rdr.GetString(3));
            allMovies.Add(new Movie(rdr.GetInt32(0), rdr.GetString(1), genre, rdr.GetInt32(4), rdr.GetString(5), rdr.GetString(6), rdr.GetInt32(7)));
        }

        private void ParseDBTo(List<Product> allProducts, NpgsqlDataReader rdr)
        {
            Product product = new Product();
            product = ParseDBTo(product, rdr);
            allProducts.Add(product);
        }

        private Product ParseDBTo(Product product, NpgsqlDataReader rdr)
        {
            MediaType mediaType = new MediaType(rdr.GetInt32(1), rdr.GetString(2));
            Genre genre = new Genre(rdr.GetInt32(5), rdr.GetString(6));
            Movie movie = new Movie(rdr.GetInt32(3), rdr.GetString(4), genre, rdr.GetInt32(7), rdr.GetString(8), rdr.GetString(9), rdr.GetInt32(10));
            
            product.Id = rdr.GetInt32(0);
            product.MediaType = mediaType;
            product.Movie = movie;
            product.Price = rdr.GetDouble(11);
            return product;
        }

    }
}
