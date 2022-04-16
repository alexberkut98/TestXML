using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML3
{
    class ListOfHeirs<T>
    {
        Heir head = null; //Головной элемент

        Heir tail = null; //Хвостовой элемент

        public void Add(string name, int depth,int depth1)
        {
            if (depth1 == 0)
            {
                Heir node = new Heir(name, depth);
                if (head == null)
                {
                    head = node;
                    head.Prev = null;
                }
                else
                {
                    tail.Next = node;
                    node.Prev = tail;
                }
                tail = node;
            }
            else
                tail.AddHeir(name, depth, (depth1 - 1));
        }

        public void Word(ref string S, int y)
        {
            Heir current = head;
            while (current != null)
            {
                current.Word(ref S, y);
                current = current.Next;
            }
        }

        //Рекурсивно пройтись по каждому элементу списка наследников данного узла,
        //дабы получить длину и ширину "рамки", на которой их можно распечатать.
        public void Sizes(ref int Length, ref int Heigth)
        {
            Heir current = head;

            //Длина коробки, принадлежащей данному узлу равняется длинне самого длинного наследника этого узла
//            int max = 0;
            while (current != null)
            {
                current.Size(ref Length, ref Heigth);

                //Все наследники данного узла печатаются в столбик после своего предка.
                //Наследники печатаются с интервалом в 1 строку.
                //Имя узла лежит на оси, делящей столбик по горизонтали пополам.
                if (current.Next != null)
                    Heigth = current.GetDown() + 2;
//                if(Length > max)
//                    max = Length;
                current = current.Next;
            }
//            Length = max;
        }
        public void Moving(int x)
        {
            Heir current = head;
            while (current != null)
            {
                current.Moving(x);
                current = current.Next;
            }
        }


        //Когда прямые потомки данного узла выстроились в столбик,
        //еадо что бы данный узел стоял на прямой, делящей этот столбик пополам
        public int GetY()
        {
            int min = head.GetY();
            int max = head.GetY();
            Heir current = head;
            while (current != null)
            {
                if(current.GetY() < min)
                    min = current.GetY();
                if(current.GetY() > max)
                    max = current.GetY();
                current = current.Next;
            }
            return (min + ((max - min) / 2));
        }

        public bool IsEmpty()
        {
            if (head == null)
                return true;
            else
                return false;
        }
    }
}
