using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML3
{
    //Один из атрибутивов некоторого узла
    class Attribute
    {
        string Name;
        string Value;   
        public Attribute(string name, string value)
        {
            Name = name;    
            Value = value;  
        }

        public Attribute Prev { get; set; }
        public Attribute Next { get; set; }
    }
}
