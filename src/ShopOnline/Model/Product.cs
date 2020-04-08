using System;
namespace ShopOnline.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string MediaType { get; set; }
        public Movie Movie { get; set; }
        public int Price { get; set; }

        public Product(int id, string mediaType, Movie movie, int price)
        {
            Id = id;
            MediaType = mediaType;
            Movie = movie;
            Price = price;
        }

        public Product()
        {
        }

        public override string ToString()
        {
            return $"{Id} - {Movie} - {MediaType}, {Price} zł";
        }
    }
}
