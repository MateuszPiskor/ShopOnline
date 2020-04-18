using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ShopOnline.Model  
{
    public class AboutUs
    {
        private Dictionary<string , string> ContactOptions;
        public Dictionary<string , string> contactOptions { get => ContactOptions; }
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
                ContactOptions = new Dictionary<string , string>();
                reader.ReadToFollowing("ContactOptions");
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                        ContactOptions.Add(reader.Name, reader.ReadString());
                        break;
                    }
                }
            }

        }
        protected void ShowContactOptions()
        {
            var i = 1;
            System.Collections.IDictionaryEnumerator dictEnum = contactOptions.GetEnumerator();
            while ( ( dictEnum.MoveNext() ) && ( dictEnum.Current != null ) )
            {
                var b = dictEnum.Value.ToString();
                var c = b.PadLeft(5 , ' '); //dictEnum.Value.ToString().PadLeft(15)
                System.Console.WriteLine("{0}) {1} -- {2}" , i++ , dictEnum.Key , c);
                
                //var x = StringFormat
            }
            Console.WriteLine();
        }

    }
}
