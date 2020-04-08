using System;
namespace ShopOnline.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ProductionYear { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public Movie()
        {
        }

        public Movie(int id, string title, string genre, int productionYear, string director, string description, int rating)
        {
            Id = id;
            Title = title;
            Genre = genre;
            ProductionYear = productionYear;
            Director = director;
            Description = description;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Title} ({ProductionYear}), {Genre}, {Rating}*";
        }
    }
}
