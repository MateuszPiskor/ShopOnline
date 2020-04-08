using System;
using System.Collections.Generic;
using ShopOnline.Model;

namespace ShopOnline.Views
{
    public class View
    {
        public View()
        {
        }

        public void PrintMovies(List<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.ToString());
            }
        }

        public void PrintProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
