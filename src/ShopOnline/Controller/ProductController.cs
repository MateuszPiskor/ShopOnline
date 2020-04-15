using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;

namespace ShopOnline.Controller
{
    public class ProductController
    {
        IProductDao productDao = new ProductDaoDB();
        ShopController shopcontroller = new ShopController();
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
                int selectedOption = Int32.Parse(view.GetUserInput("Select option: "));
                
                switch(selectedOption)
                {
                    case 0:
                        ChangeProductControllerToInactive();
                        break;
                    case 1:
                        view.PrintMovies(productDao.GetAllMovies());
                        shopcontroller.runShopController();
                        break;
                    case 2:
                        view.PrintProducts(productDao.GetAllProducts());
                        shopcontroller.runShopController();
                        break;
                    case 3:
                        GetProductsByInsertedTitle();
                        shopcontroller.runShopController();
                        break;
                    case 4:
                        GetProductsByMediaType();
                        shopcontroller.runShopController();
                        break;
                    case 5:
                        GetProductsByGenre();
                        shopcontroller.runShopController();
                        break;
                    case 6:
                        GetProductsByDirector();
                        shopcontroller.runShopController();
                        break;
                    case 7:
                        GetProductsByRating();
                        shopcontroller.runShopController();
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
            view.PrintProducts(productDao.GetProductsByTitlePart(title));
        }

        private void GetProductsByMediaType()
        {
            view.PrintDictionary(productDao.GetAllMediaTypes());
            int selectedMediaTypeNumber = Int32.Parse(view.GetUserInput("Choose media type's number: "));
            view.PrintProducts(productDao.GetProductsByMediaType(selectedMediaTypeNumber));
        }

        private void GetProductsByGenre()
        {
            view.PrintDictionary(productDao.GetAllGenres());
            int selectedGenreNumber = Int32.Parse(view.GetUserInput("Choose genre's number: "));
            view.PrintProducts(productDao.GetProductsByGenre(selectedGenreNumber));
        }

        private void GetProductsByDirector()
        {
            string director = view.GetUserInput("Enter director name (e.g. David Lynch: ");
            view.PrintProducts(productDao.GetProductsByDirector(director));
        }

        private void GetProductsByRating()
        {
            string ratings = view.GetUserInput("Enter ratings separated by comma, e.g. '6, 7, 8': ");
            string[] arrayOfRatings = ratings.Split(',');
            foreach (string rating in arrayOfRatings)
            {
                view.PrintMessage($"Rsting {rating}:");
                view.PrintProducts(productDao.GetProductsByRating(Int32.Parse(rating)));
            }
        }
    }
}
