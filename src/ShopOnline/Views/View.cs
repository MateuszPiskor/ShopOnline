﻿using System;
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

        public void PrintBasket(List<CartItem> cartItems,Cart cart)
        {
            Console.WriteLine("\nYour Basket: " );
            foreach (CartItem cartItem in cartItems)
            {
                Console.WriteLine(cartItem.ToString());
            }
            Console.WriteLine($"Total : {cart.TotalPrice}");
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
        public void PrintDictionary(Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> element in dictionary)
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

        public void PrintPayments(List<Payment> payments)
        {
            foreach (Payment payment in payments)
            {
                Console.WriteLine(payment.ToString());
            }
            Console.WriteLine();
        }

        public void PrintDeliveries(List<Delivery> deliveries)
        {
            foreach (Delivery delivery in deliveries)
            {
                Console.WriteLine(delivery.ToString());
            }
            Console.WriteLine();
        }

        public void PrintOrderConfirmation(List<CartItem> cartItems, Order order)
        {
            Console.WriteLine();
            Console.WriteLine($"Order number {order.Id}:");
            Console.WriteLine($"Date: {order.Date.ToString("dd/MM/yyyy")}");
            Console.WriteLine("--------------------------");
            PrintBasket(cartItems, order.Cart);
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Total price: {order.TotalPrice} zł");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
        }

        public void DisplayMenu(string[] rows)
        {
            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }
        }

        public string GetAnswer()
        {
            Console.WriteLine("Your choice: ");
            return Console.ReadLine();
        }


        public string GetPassword()
        {
            PrintMessage("Get passwor; ");
            string pass = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return pass;
        }
    }
}
