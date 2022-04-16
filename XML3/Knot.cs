using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML3
{
    //Здесь будет храниться имя каждого из обьектов таблицы и его глубина расположения
    class Knot
    {
        string Name;
        int Depth;
        public Knot(string name,int depth)
        {
            Name = name;    
            Depth = depth;  
        }

        //Добавление аттрибута в список принадлежащих к данному узлу

        public Knot Prev { get; set; }
        public Knot Next { get; set; }

        public int GetDepth() { return Depth; }
        public string GetName() { return Name; }
    }
}
