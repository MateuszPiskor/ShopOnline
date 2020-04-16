using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ShopOnline.Model  
{
    public class AboutUs
    {
        private List<string> ContactOptions; 
        public List<string> contactOptions { get => ContactOptions; }
        public AboutUs()
        {   
            LoadContactOptionsToList();
        }
        public void ShowAboutUsDetails(string DetailFromXml)
        {
            using (XmlReader reader = XmlReader.Create("DataAccess/AboutUs.xml"))
            {
                reader.ReadToFollowing(DetailFromXml);
                Console.WriteLine(reader.ReadElementContentAsString());
            }
        }
        public void LoadContactOptionsToList()
        {
            using (XmlReader reader = XmlReader.Create("DataAccess/AboutUs.xml"))
            {
                ContactOptions = new List<string>();
                reader.ReadToFollowing("ContactOptions");
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                        ContactOptions.Add(reader.Name);
                        break;
                    }
                }
            }

        }
        protected void ShowContactOptions()
        {
            for (int i=0; i < contactOptions.Count; i++)
            {
                Console.WriteLine($"({i+1}) --> {contactOptions[i]}");
            }
        }

    }
}
