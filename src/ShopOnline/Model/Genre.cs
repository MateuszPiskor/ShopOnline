using System;
namespace ShopOnline.Model
{
    public class Genre
    {
        private int v1;
        private string v2;

        public int Id { get; set; }
        public string Name { get; set; }

        public Genre()
        {
        }

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
