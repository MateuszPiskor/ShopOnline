using System;
namespace ShopOnline.Model
{
    public class Customer
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public CustomerDetails Details { get; set; }
        public bool Registered;
        internal string Pass;


        public Customer(int id, string firstName, string lastName, CustomerDetails details,
                        bool isRegistered, string password)
        {

            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Details = details;
            Registered = isRegistered;
            Pass = password;


        }
        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {Details} {Registered} {Pass}";
        }
        




    }
}
