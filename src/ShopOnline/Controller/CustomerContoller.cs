using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;


namespace ShopOnline.Controller
{
    public class CustomerContoller
    {
        ICustomerDao customerDao = new CustomerDaoDB();
        bool isLoggedIn;
        int userId = -1;

        Dictionary<int, string> menuOptions = new Dictionary<int, string>()
        {
            {0, "Go Back "},
            {1, "Log In"},
            {2, "Log Off"},
            {3, "Registration"},
            {4, "Shopping without registration" }
        };

        View view = new View();

        public CustomerContoller()
        {

        }

        public int Login()
        {
            int errorCounter = 0;
            do
            {
                string login = view.GetUserInput("Enter Your email: ");
                string pass = view.GetUserInput("Enter Your password: ");
                bool loginOk = false;

                Customer user;
                try
                {
                    user = customerDao.GetCustomerByEmail(login);
                    if (pass.Equals(user.Pass))
                    {
                        loginOk = true;
                        isLoggedIn = true;
                        return userId = user.Id;

                    }
                    else
                    {
                        errorCounter++;
                        view.PrintMessage("Wrong email or password. Try again.");
                    }
                }
                catch (IdNotFoundException)
                {
                    errorCounter++;
                    view.PrintMessage("Wrong email or password. Try again.");

                }
            } while(errorCounter < 3);

            view.PrintMessage("Number of tries exceeded.");
            return -1;
            


        }


        public void Logoff()
        {
            isLoggedIn = false;
            view.PrintMessage("You have been logged Off.");
        }

        public int registerNewCustomer()
        {

            bool emailOk = false;
            string email;

            while (!emailOk)
            {
                view.PrintMessage("Enter Your email:");
                email = view.GetUserInput("E-mail: ").ToLower();
                if(IsValidEmail(email))
                {
                   
                    try
                    {
                        var user = customerDao.GetCustomerByEmail(email);



                    } catch(IdNotFoundException)
                    {

                        emailOk = true;
                    }
                }
                else
                {
                    view.PrintMessage("Wrong email format.");
                }


            }




            view.PrintMessage("Enter Your data.");
            string firstName = view.GetUserInput("First name: ");
            firstName = ToCapitalFirstLetter(firstName);
            string lastName = view.GetUserInput("Last name: ");
            lastName = ToCapitalFirstLetter(lastName);
            string city = view.GetUserInput("City: ");
            city = ToCapitalFirstLetter(city);
            string postCode = view.GetUserInput("Postcode XX-XXX: ");
            string street = view.GetUserInput("Street: ");
          
            string phone = view.GetUserInput("Phone number: ");
            string password = view.GetUserInput("Password: ");

            

        }

        private string ToCapitalFirstLetter(string text)
        {
            text = text.ToLower();
            text = text.Substring(0, 1).ToUpper() + text.Substring(1, text.Length - 1);
            return text;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }






    }
}
