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

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }

        public void PrintMovies(List<Movie> movies)
        {
            foreach(Movie movie in movies)
            {
                Console.WriteLine(movie.ToString());
            }
            Console.WriteLine();
        }

        public void PrintProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine();
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

        public void PrintDictionary(Dictionary<int, string> dictionary)
        {
            foreach (KeyValuePair<int, string> element in dictionary)
            {
                Console.WriteLine($"{element.Key}) {element.Value}");
            }
            Console.WriteLine();
        }

        public string GetUserInput(string message)
        {
            Console.Write(message);
            string output = Console.ReadLine();
            return output;
        }
    }
}
