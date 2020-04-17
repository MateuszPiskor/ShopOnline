using System;
using System.Collections.Generic;
using System.Linq;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class ProductController
    {
        IProductDao productDao = new ProductDaoDB();
        ShopController shopController { get; set; }
        bool isProductControllerActive = true;
        Dictionary<int, string> requests = new Dictionary<int, string>()
        {
            {0, "Go back" },
            {1, "Show available movies" },
            {2, "Show all products" },
            {3, "Search by movie title" },
            {4, "Filter by media type" },
            {5, "Filter by genre" },
            {6, "Filter by director" },
            {7, "Filter by rating" }
        };

        View view = new View();


        public ProductController()
        {
        }

        public void RunProductController()
        {
            while (isProductControllerActive)
            {
                view.PrintDictionary(requests);
                int selectedOption = GetAnOptionFromMenu("Choose option: ", requests);
                
                switch(selectedOption)
                {
                    case 0:
                        ChangeProductControllerToInactive();
                        break;
                    case 1:
                        view.PrintMovies(productDao.GetAllMovies());
                        break;
                    case 2:
                        view.PrintProducts(productDao.GetAllProducts());
                        StartShopController();
                        break;
                    case 3:
                        GetProductsByInsertedTitle();  
                        break;
                    case 4:
                        GetProductsByMediaType();
                        break;
                    case 5:
                        GetProductsByGenre();
                        break;
                    case 6:
                        GetProductsByDirector();
                        break;
                    case 7:
                        GetProductsByRating();
                        break;
                    default:
                        view.PrintMessage("Choose one of the available options");
                        break;
                }
            }
        }

        private void ChangeProductControllerToInactive()
        {
            isProductControllerActive = false;
        }

        private void GetProductsByInsertedTitle()
        {
            string title = view.GetUserInput("Enter title or part of it: ");
            List<Product> filteredProducts = productDao.GetProductsByTitlePart(title);
            DisplayFilteredProducts(filteredProducts);
        }

        private void GetProductsByMediaType()
        {
            Dictionary<int, string> mediaTypes = productDao.GetAllMediaTypes();
            view.PrintDictionary(mediaTypes);
            int selectedMediaTypeNumber = GetAnOptionFromMenu("Choose media type's number: ", mediaTypes);
            view.PrintProducts(productDao.GetProductsByMediaType(selectedMediaTypeNumber));
            StartShopController();
        }

        private void GetProductsByGenre()
        {
            Dictionary<int, string> genres = productDao.GetAllGenres();
            view.PrintDictionary(genres);
            int selectedGenreNumber = GetAnOptionFromMenu("Choose genre's number: ", genres);
            view.PrintProducts(productDao.GetProductsByGenre(selectedGenreNumber));
            StartShopController();
        }

        private void GetProductsByDirector()
        {
            string director = view.GetUserInput("Enter director name (e.g. David Lynch): ");
            List<Product> filteredProducts = productDao.GetProductsByDirector(director);
            DisplayFilteredProducts(filteredProducts);
        }

        private void GetProductsByRating()
        {
            string ratings = view.GetUserInput("Enter ratings separated by comma, e.g. '6, 7, 8': ");
            string[] arrayOfRatings = ratings.Split(',');
            foreach (string rating in arrayOfRatings)
            {
                view.PrintMessage($"Rating {rating}:");
                view.PrintProducts(productDao.GetProductsByRating(Int32.Parse(rating)));
            }
            StartShopController();
        }

        private void StartShopController()
        {
            shopController = new ShopController();
            shopController.runShopController();
        }

        private int GetAnOptionFromMenu(string message, Dictionary<int, string> dictMenu)
        {
            while (true)
            {
                string input = view.GetUserInput(message);

                if (Int32.TryParse(input, out int number) && dictMenu.ContainsKey(Int32.Parse(input)))
                {
                    int output = Int32.Parse(input);
                    return output;
                }
                else
                {
                    view.PrintMessage($"Please enter a number between {dictMenu.Keys.Min()} and {dictMenu.Keys.Max()}");
                }
            }           
        }

        private void DisplayFilteredProducts(List<Product> products)
        {
            if (products.Any())
            {
                view.PrintProducts(products);
                StartShopController();
            }
            else
            {
                view.PrintMessage("Nothing found.");
            }
        }

    }
}
