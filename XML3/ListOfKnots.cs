using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML3
{
    class ListOfKnots
    {
        int Length = 0;
        int Heigth = 0;

        Knot head = null; //Головной элемент

        Knot tail = null; //Хвостовой элемент

        ListOfHeirs<Heir> list = new ListOfHeirs<Heir>();

        //Глубина, на которую нам надо погрузиться в рекурсию наследников
        int depth = 0;
        public void Add(string name, int depth)
        {
            Knot node = new Knot(name, depth);
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


        //Формируем из списка дерево, в котором у каждого элемента будут свои наследники
        public void SearchHeirs()
        {
            Knot current = head;
            Knot current2 = head.Next;

            list.Add(current.GetName(), current.GetDepth(),depth);
            while(current!= null)
            {
                string Name = current.GetName();

                //Закрывающие скобки в дерево включаться не будут
                if (Name[0] != '/')
                {
                    //Если следующий элемент глубже текущего - он его наследник
                    if (current.GetDepth() < current2.GetDepth())
                    {
                        list.Add(current2.GetName(), current2.GetDepth(),depth+1);
                        current= current2;
                        current2 = current2.Next;
                        depth++;
                    }
                    //Мы достигли конца ветки, надо отойти назад
                    if (current.GetDepth() > current2.GetDepth())
                    {
                        Name = current2.GetName();
                        while (Name[0] == '/')
                        {
                            current2 = current2.Next;
                            if (current2 == null)
                                break;
                            Name = current2.GetName();
                        }
                        if (current2 != null)
                        {
                            while (current.GetDepth() != current2.GetDepth())
                                current = current.Prev;
                            depth = current.GetDepth();
                        }
                        else
                            break;
                    }

                    if (current.GetDepth() == current2.GetDepth())
                    {
                        Name = current2.GetName();
                        if (Name[0] != '/')
                        {
                            list.Add(current2.GetName(), current2.GetDepth(), depth);
                            current = current2;
                            current2 = current2.Next;
                        }
                        else
                        {

                            /*
                             * Пояснение:
                             * Обычно, когда осуществляется "погружение" в создающееся дерево, оно заканчивается на некоем элементе,
                             * который сам в себе хранит закрывающую скобку, а потому следующий элемент списка 
                             * (на основе которого и создается дерево), является либо его соседом с тем же значением GetDepth()
                             * (данный случай обрабатывается в блоке 85 - 90), либо закрывающей скобкой узла, чье значение GetDepth() меньше,
                             * чем у текущего (данный случай обрабатывается в блоке 54 - 60).
                             * Однако бывают ситуации, когда между открывающим и закрывающим узлами хранится не новый узел, а просто некий текст,
                             * который программа не видит (пример: "<interfaces>127.0.0.1,127.0.0.1,127.0.0.1</interfaces>").
                             * Это приводит к ситуациям, когда узел с закрывающей скобкой, вместо того, чтоб иметь значение GetDepth() меньшее,
                             * чем у предыдущего, считанного программой, элемента списка (а чаще всего, повторюсь, происходит именно это).
                             * Данный блок кода предназначен для обработки именно таких ситуаций.
                             */
                            while (Name[0] == '/')
                            {
                                current2 = current2.Next;
                                if (current2 == null)
                                    break;
                                Name = current2.GetName();
                            }
                            if (current2 != null)
                            {
                                while (current.GetDepth() != current2.GetDepth())
                                    current = current.Prev;
                                depth = current.GetDepth();
                            }
                            else
                                break;
                        }
                    }
                }
            }
        }

        //Вычисляем размеры участка консоли, на котором будет печататься дерево
        public void CheckSizes()
        {
            list.Sizes(ref Length, ref Heigth);
        }

        //Вывод на экран консоли итогового дерева.
        //Планируется построчный вывод элементов дерева, в соответствии с их координатами.
        public void Print()
        {
            string S = "";
            for (int i = 0; i <= Heigth; i++)
            {
                list.Word(ref S, i);

                if (S != "")
                    Console.Write(S);
                Console.Write('\n');
                S = "";
            }
        }
    }

}
