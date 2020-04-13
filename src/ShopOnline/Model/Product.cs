using System;
namespace ShopOnline.Model
{
    public class Product
    {
        public int Id { get; set; }
        public MediaType MediaType { get; set; }
        public Movie Movie { get; set; }
        public int Price { get; set; }

        public Product(int id, MediaType mediaType, Movie movie, int price)
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
            return $"{Id} - {Movie} - {MediaType.Name}, {Price} zł";
        }
    }
}
