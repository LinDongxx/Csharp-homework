using System;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("calculator is loading...");
            float a,b,c=0;
            string num1,num2,ope;
            Console.WriteLine("please enter the first number.");
            num1=Console.ReadLine();
            Console.WriteLine("please enter the second number.");
            num2= Console.ReadLine();
            Console.WriteLine("please enter the operator");
            ope =Console.ReadLine();
            a = float.Parse(num1);
            b = float.Parse(num2);
            if (ope == "+")
            {
                c = a + b;
            }
            else if (ope == "-")
            {
                c = a - b;
            }
            else if (ope == "/")
            {
                c = a / b;
            }
            else if (ope == "*")
            {
                c = a * b;
            }
            Console.WriteLine("the result is " + c);

        }
    }
}
