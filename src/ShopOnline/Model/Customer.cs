using System;
namespace ShopOnline.Model
{
    public class Customer
    {
        public int id { get; }
        public string first_name { get; }
        public string last_name { get; }
        public CustomerDetails details { get; set; }
        public bool registret;
        private string pass;


        public Customer(int id, string first_name, string last_name, CustomerDetails details,
                        bool is_registered, string password)
        {

            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.details = details;
            this.registret = is_registered;
            this.pass = password;


        }
        public override string ToString()
        {
            return $"{id} {first_name} {last_name} {details} {registret} {pass}";
        }
        




    }
}
