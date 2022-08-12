using System;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class RSA
    {
        public int GreatestCommonDivisor(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                    a = a - b;
                else
                    b = b - a;
            }
            return a;
        }

        public (int a, int xPrev, int yPrev) ExtendedEuclidesAlgorithm(int a, int b)
        {
            (int x, int xPrev, int y, int yPrev) = (0, 1, 1, 0);
            while (b != 0)
            {
                int floorQuotient = a;
                (a, b) = (b, a - b * floorQuotient);
                (xPrev, x) = (x, xPrev - x * floorQuotient);
                (yPrev, y) = (y, yPrev - y * floorQuotient);
            }

            return (a, xPrev, yPrev);
        }
    }
}
