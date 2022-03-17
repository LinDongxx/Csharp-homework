using System;

namespace week04
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> intlist = new GenericList<int>();
            for (int x = 0; x < 10; x++)
            {
                intlist.Add(x);
            }
            Console.WriteLine("打印元素：");
            intlist.ForEach(i => Console.WriteLine(i));
            Console.WriteLine("求最大值:");
            int max = 0;
            intlist.ForEach(delegate (int i) { if (max < i) max = i; });
            Console.WriteLine(max);
            Console.WriteLine("求和:");
            int sum = 0;
            intlist.ForEach(i => sum += i);
            Console.WriteLine(sum);
            Console.WriteLine("求最小值");
            int min = 0;
            intlist.ForEach(delegate(int i) { if (min > i) min = i; });
            Console.WriteLine(min);
        }

        public class Node<T>
        {
            public Node<T> Next { get; set; }
            public T Data { get; set; }
            public Node(T t)
            {
                Next = null;
                Data = t;
            }
        }
        public class GenericList<T>
        {
            private Node<T> head;
            private Node<T> tail;
            public GenericList() { head = null; tail = null; }
            public Node<T> Head { get { return head; } }
            public void Add(T t)
            {
                Node<T> n = new Node<T>(t);
                if (tail == null) { head = n; tail = n; }
                else { tail.Next = n; tail = n; }
            }
            public void ForEach(Action<T> action)
            {
                Node<T> temp = head;
                while (temp.Next != null)
                {
                    action(temp.Data);
                    temp = temp.Next;
                }
                action(temp.Data);
            }
        }

    }
}

  
