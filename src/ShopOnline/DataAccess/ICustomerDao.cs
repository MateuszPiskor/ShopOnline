using System;
using System.Collections.Generic;
using ShopOnline.Model;


namespace ShopOnline.DataAccess
{
    public interface ICustomerDao
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void RemoveCustomer(int id);
        void InsertNewCustomer(string firstName, string lastName, string city, string postCode, string street, string email, string phone,
                        bool isRegistered, string passwo);
        void UpdateCustomerDetails(string city, string postCode, string street, string email, string phone,int id);
        void UpdateCustomerPhoneNumber(string newPhone, int id);
        void UpdateCustomerEmail(string newEmail, int id);
        void UpdateCustomerStreet(string newStreet, int id);
        void UpdateCustomerPosteCode(string newPostCode, int id);
        void UpdateCustomerCity(string newCity, int id);
        Customer GetCustomerByEmail(string email);

    }
}
