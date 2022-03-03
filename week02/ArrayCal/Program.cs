using System;

namespace ArrayCal
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = Convert.ToInt32(Console.ReadLine());      
            int[] arry = new int[length];
            for (int i = 0; i < length; i++)
            {
                arry[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("the max number is" + ArrayMax(arry));
            Console.WriteLine("the min number is" + ArrayMin(arry));
            Console.WriteLine("the sum number is" + ArraySum(arry));
            Console.WriteLine("the aver number is" + ArrayAver(arry));

        }

        public static int ArrayMax(int[] a)
        {
            
                int max = a[0];
                for (int i = 1; i < a.Length; i++)
                {
                    if (max < a[i])
                    {
                        max = a[i];
                    }
                }
                return max;
            
        }
        public static int ArrayMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (min > a[i])
                {
                    min = a[i];
                }
            }
            return min;
        }
        public static float ArraySum(int[] a)
        {
            int sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i];
            }
            return sum;

        }
        public static float ArrayAver(int[] a)
        {
            float aver = 0;
            
            aver=ArraySum(a)/ a.Length ;
            return aver;

        }
       

    }
}

