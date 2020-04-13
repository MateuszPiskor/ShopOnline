using System;
namespace ShopOnline.Model
{
    public class CustomerDetails
    {
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }



        public CustomerDetails(string city, string postCode, string street, string email, string phone)
        {
            City = city;
            PostCode = postCode;
            Street = street;
            Email = email;
            Phone = phone;

        }

        public CustomerDetails()
        {
            City = "unknown";
            PostCode = "unknown";
            Street = "unknown";
            Email = "unknown";
            Phone = "unknown";
        }
        
        public override string ToString()
        {
            return $"{City} {PostCode} {Street} {Email} {Phone}";
        }
    }
}
