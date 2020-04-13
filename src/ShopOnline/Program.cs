using System;
using ShopOnline.DataAccess;
using ShopOnline.Views;
using ShopOnline.Model;
using System.Collections.Generic;


namespace ShopOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Customer> cust = new List<Customer>();
            ICustomerDao dao = new CustomerDaoDB();
            //var temp = dao.GetCustomerById(2);
            //foreach (var ele in temp)
            //{
            //    Console.WriteLine(ele);
            //}
            //try
            //{
            //    var customer = dao.GetCustomerById(2);
            //    Console.WriteLine(customer);

            //}
            //catch(IdNotFoundException e)
            //{
            //    Console.WriteLine(e);
            //}
            //dao.RemoveCustomer(2);
            //dao.UpdateCustomerDetails("Krakow", "02-122", "Drzewo", "mn@onet.pl", "000000000", 1);
            //dao.InsertNewCustomer("Jan", "Kowalski", "Krakow", "09-333", "Tulipan", "J@onet.pl", "999888777", false, "wwww");
            //var temp = dao.GetCustomerByEmail("J@onet.pl");

            //try
            //{
            //    var customer = dao.GetCustomerByEmail("J@onet.pl");
            //    Console.WriteLine(customer);
            //}
            //catch(IdNotFoundException e)
            //{
            //    Console.WriteLine(e);
            //}
            dao.UpdateCustomerCity("Sopot", 10);


        }
    }
}
