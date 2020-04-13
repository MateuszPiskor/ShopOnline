using System;
using System.Collections.Generic;
using ShopOnline.Model;

namespace ShopOnline.Views
{
    public class View
    {
        public void displayMenu(string [] rows)
        {
            foreach(var row in rows)
            {
                Console.WriteLine(row);
            }
        }

        public void PrintMovies(List<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.ToString());
            }
        }

        public string getAnswer()
        {
            Console.WriteLine("You chosse: ");
            return Console.ReadLine();
        }

        public void PrintProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void PrintMovie(Movie movie)
        {
            Console.WriteLine();
            Console.WriteLine(movie.ToString());
            Console.WriteLine($"Director: {movie.Director}");
            Console.WriteLine($"Story:");
            Console.WriteLine(movie.Description);
            Console.WriteLine();
        }
    }
}
