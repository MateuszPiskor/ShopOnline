using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace options
{
    public class AboutUs
    {
        public List<string> ContactOptions {get; set;}
        public AboutUs()
        {
            ContactOptions = new List<string>();
            LoadContactOptionsToList();
        }
        public string ShowAboutUsDetails(string DetailFromXml)
        {
            using (XmlReader reader = XmlReader.Create("DataAccess/AboutUs.xml"))
            {
                reader.ReadToFollowing(DetailFromXml);
                return reader.ReadElementContentAsString();
            }
        }
        private void LoadContactOptionsToList()
        {
            using (XmlReader reader = XmlReader.Create("DataAccess/AboutUs.xml"))
            {
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

    }
}
