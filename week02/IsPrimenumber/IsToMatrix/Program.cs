using System;

namespace IsToMatrix
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[,] TestMatrix = null;

            TestMatrix = new int[,] {{ 1, 2, 3, 4 },
                           { 4, 1, 2, 3},
                           { 3, 4, 1, 2}
        };
            var res = IsToeplitzMatrix(TestMatrix);
            Console.WriteLine(res);

            Console.ReadKey();
        }

        private static bool IsToeplitzMatrix(int[,] matrix)
        { 
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i + 1, j + 1] != matrix[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
