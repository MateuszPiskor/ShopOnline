using System;
namespace ShopOnline.Model
{
    public class CustomerDetails
    {
        public string city { get; set; }
        public string postCode { get; set; }
        public string street { get; set; }
        public string email { get; set; }
        public string phone { get; set; }



        public CustomerDetails(string city, string postCode, string street, string email, string phone)
        {
            this.city = city;
            this.postCode = postCode;
            this.street = street;
            this.email = email;
            this.phone = phone;

        }

        public CustomerDetails()
        {
            city = "unknown";
            postCode = "unknown";
            street = "unknown";
            email = "unknown";
            phone = "unknown";
        }
        
        public override string ToString()
        {
            return $"{city} {postCode} {street} {email} {phone}";
        }
    }
}
