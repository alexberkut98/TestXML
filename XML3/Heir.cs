using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML3
{
    //Элемент списка наследников данного узла
    class Heir
    {
        int pr = 0;

        string Name;
        int Depth;

        //Число отступов которое будет сделано вконсоли при печати данного слова
        int X = 0;
        int Y = 0;

        //Координаты верхней и нижней точек блока наследников, принадлежащих данному узлу
        int UP = 0;
        public int DOWN = 0;

        //Каждый наследник обладает линейным списком своих наследников
        ListOfHeirs<Heir> list = new ListOfHeirs<Heir>();
        public Heir(string name, int depth)
        {
            Name = name;
            Depth = depth;
        }

        public void AddHeir(string name, int depth, int depth1)
        {
            list.Add(name, depth, depth1);
        }

        public Heir Prev { get; set; }
        public Heir Next { get; set; }

        public void Size(ref int Length, ref int Heigth)
        {
            UP = Heigth;
            if (!list.IsEmpty())
            {
                pr++;
                list.Sizes(ref Length, ref Heigth);
                list.Moving((Name.Length + 1) * 2);
            }

            //Как только в данном узле закончится работа над его наследниками,
            //узел сам должен стать частью коробки (+1 для разрыва между узлом и наследниками, пригодится в печати).
            Y = Heigth;

            //При этом смещать узел к линии, делящий предполагаемый блок наследников пополам
            if (pr > 0)
            {
                Y = list.GetY();
            }

            if (UP == Heigth)
                Y = UP;

            DOWN = Heigth;
            //Если у нас есть наследники, координаты, точек, где они будут напечатаны,
            //должны быть смещены по оси Х на длинну имени узла плюс еденичный разрыв.

            //Каждый узел должен точно знать высоты верхней и нижней точки блока принадлежащих ему наследников

        }

        //Когда узел закончит работу с одним из своих наследников, он должен будет знать границы созданной им "коробки",
        //дабы работа со следующим наследником могла осуществляться с соответствующим отступом.
        public int GetDown()
        {
            return DOWN;
        }

        public void Moving(int x)
        {
            if (!list.IsEmpty())
                list.Moving(x);
            X += x;
        }

        public int GetY()
        {
            return Y;
        }

        public void Word(ref string S, int y)
        {
            if (Y == y)
            {
                while (S.Length < X)
                    S += " ";
                S += Name;
            }
            list.Word(ref S, y);
        }
    }
}
