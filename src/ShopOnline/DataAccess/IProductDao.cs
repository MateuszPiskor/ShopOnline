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

    }
}
