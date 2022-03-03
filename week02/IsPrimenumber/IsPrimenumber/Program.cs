using System;

namespace IsPrimenumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please enter a integer");
            int num = Convert.ToInt32(Console.ReadLine());
            for (int i = 2; i < num; i++)
            {
                while (num != i)
                {
                    if (num % i == 0)
                    {
                        Console.WriteLine(i);
                        num = num / i;
                    }
                    else
                    {
                        i++;
                        if (num % i == 0)
                        {
                            Console.WriteLine(i);
                            num = num / i;
                        }
                        if (i == num) break;
                    }
                }
            }
        }
    }
}
