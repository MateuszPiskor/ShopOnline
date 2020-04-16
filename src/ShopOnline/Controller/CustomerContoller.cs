using System;
using System.Collections.Generic;
using ShopOnline.DataAccess;
using ShopOnline.Model;
using ShopOnline.Views;


namespace ShopOnline.Controller
{
    public class CustomerController
    {
        ICustomerDao customerDao;
        Cart cart;
        private bool isLoggedIn;
        private int userId;
        public bool IsLoggedIn //read only!
        {
            get { return isLoggedIn;  }
        }
        public int UserId //read only!
        {
            get { return userId; }
        }

        Dictionary<int, string> menuOptions = new Dictionary<int, string>()
        {
            {0, "Go Back "},
            {1, "Log In"},
            {2, "Log Off"},
            {3, "Registration"},
            {4, "Shopping without registration" }
        };
        Dictionary<int, string> updateOptions = new Dictionary<int, string>()
        {
            {0, "Exit "},
            {1, "Uptade Phone Number"},
            {2, "Update Poste Code"},
            {3, "Update City" },
            {4, "Update Street" }
            
        };

        View view = new View();

        //Composition
        public CustomerController(Cart cart)
        {
            customerDao = new CustomerDaoDB();
            isLoggedIn = false;
            userId = -1;
            this.cart = cart;
        }

        //Agregation
        public CustomerController(CustomerDaoDB customerDao)
        {
            this.customerDao = customerDao;
            isLoggedIn = false;
            userId = -1;
        }

        public void RunMenu()
        {

            int selectedOption = 0;

            do
            {
                view.PrintDictionary(menuOptions);
                try
                {
                    selectedOption = Int32.Parse(view.GetUserInput("Select option: "));
                    switch (selectedOption)
                    {
                        case 0:
                            view.PrintMessage("Exiting menu.");
                            break;
                        case 1:
                            Login();
                            break;
                        case 2:
                            Logoff();
                            break;
                        case 3:
                            RegisterNewCustomer(true);
                            break;
                        case 4:
                            RegisterNewCustomer(false);
                            var orderController = new OrderController(cart);
                            orderController.RunOrderController();
                            break;

                    }
                }
                catch (ArgumentNullException)
                {
                    view.PrintMessage("No argument detected");
                }
                catch (FormatException)
                {
                    view.PrintMessage("Wrong format!");
                }
                catch (OverflowException)
                {
                    view.PrintMessage("Argument too big!");
                }


            } while (selectedOption != 0);
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
                        userId = user.Id;

                        view.PrintMessage("Login successful.");
                        string decision = view.GetUserInput("Do you wont to update Your data? Press 'y' " +
                                                            "or 'yes' to enter Your account").ToLower();
                        if (decision.Equals("y") || decision.Equals("yes"))
                            UpdateCustomerDetails();

                        return userId;

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



        public int RegisterNewCustomer(bool setAsRegistered)
        {

            bool emailOk = false;
            string email = "";

            while (!emailOk)
            {
                view.PrintMessage("Enter Your email:");
                email = view.GetUserInput("E-mail: ").ToLower();
                if(IsValidEmail(email))
                {
                   
                    try
                    {
                        var user = customerDao.GetCustomerByEmail(email);
                        string decision = view.GetUserInput("Your email exist in database. " +
                                                            "Press 'q' to exit or 'Enter' to try again.").ToLower();
                        if(decision.Equals("q"))
                        {
                            return -1;
                        }
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
            street = ToCapitalFirstLetter(street);
            string phone = view.GetUserInput("Phone number: ");
            string password = "default";

            if(setAsRegistered)
            {
                password = view.GetUserInput("Password: ");
            } 

            customerDao.InsertNewCustomer(firstName, lastName, city, postCode, street, email, phone, setAsRegistered, password);
            try
            {
                Customer newUser = customerDao.GetCustomerByEmail(email);
                userId = newUser.Id;
                return newUser.Id;

            } catch(IdNotFoundException)
            {
                return -1;
            }
            
        }


        public void UpdateCustomerDetails()
        {
            int selectedOption = 0;

            do
            {
                view.PrintDictionary(updateOptions);
                try
                {
                    selectedOption = Int32.Parse(view.GetUserInput("Select option: "));
                    switch (selectedOption)
                    {
                        case 0:
                            view.PrintMessage("Exiting menu.");
                            break;
                        case 1:
                            var phoneNumber = view.GetUserInput("Enter your new phone number: ");
                            customerDao.UpdateCustomerPhoneNumber(phoneNumber, userId);
                            break;
                        case 2:
                            var posteCode = view.GetUserInput("Enter your new poste code: ");
                            customerDao.UpdateCustomerPosteCode(posteCode, userId);
                            break;
                        case 3:
                            var city = view.GetUserInput("Enter your new city: ");
                            city = ToCapitalFirstLetter(city);
                            customerDao.UpdateCustomerCity(city, userId);
                            break;
                        case 4:
                            var street = view.GetUserInput("Enter your new street: ");
                            street = ToCapitalFirstLetter(street);
                            customerDao.UpdateCustomerStreet(street, userId);
                            break;

                    }
                } catch(ArgumentNullException)
                {
                    view.PrintMessage("No argument detected");
                } catch(FormatException)
                {
                    view.PrintMessage("Wrong format!");
                } catch(OverflowException)
                {
                    view.PrintMessage("Argument too big!");
                }


            } while(selectedOption != 0);

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
        public Customer GetCustomer(int id)
        {
            return customerDao.GetCustomerById(id);

        }

        public Customer GetCustomer()
        {
            return GetCustomer(userId);
        }

    }
}
