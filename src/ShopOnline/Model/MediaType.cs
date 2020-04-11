using System;
namespace ShopOnline.Model
{
    public class MediaType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MediaType()
        {
        }

        public MediaType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
