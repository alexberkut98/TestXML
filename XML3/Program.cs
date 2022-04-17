using System;
using System.Xml;

namespace XML3
{
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class Class1
    {

        static void Main(string[] args)
        {
            string URLString = "https://raw.githubusercontent.com/palnikovms/DataForTestingTask/master/tree.xml";
            XmlTextReader reader = new XmlTextReader(URLString);

            //Первичный список всех узлов (с их атрибутивами), который уже потом будет превращаться в дерево
            ListOfKnots list = new ListOfKnots();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        list.Add(reader.Name, reader.Depth);  
                        break;

                    case XmlNodeType.EndElement:
                        list.Add("/"+ reader.Name, reader.Depth);
                        break;
                }
            }
            list.SearchHeirs();
            list.CheckSizes();
            list.Print();
        }

        
    }
}