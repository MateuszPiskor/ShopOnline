using System;
using System.Collections.Generic;
using ShopOnline.Model;

namespace ShopOnline.DataAccess
{
    public interface IProductDao
    {
        List<Movie> GetAllMovies();
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> GetProductsByTitlePart(string partOfTitle);
        List<Product> GetProductsByGenre(int genreId);
        List<Product> GetProductsByMediaType(int mediaTypeId);
        List<Product> GetProductsByDirector(string director);
        List<Product> GetProductsByRating(int rating);
        Dictionary<int, string> GetAllGenres();
        Dictionary<int, string> GetAllMediaTypes();

    }
}
