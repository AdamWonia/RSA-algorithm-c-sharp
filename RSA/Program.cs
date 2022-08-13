using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

        public (int a, int xPrev, int yPrev) ExtendedEuclideanAlgorithm(int a, int b)
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

        public int CountE(int phi)
        {
            Random rand = new Random();
            while (true)
            {
                int e = rand.Next(2, phi);
                if (GreatestCommonDivisor(e, phi) == 1)
                {
                    return e;
                }
            }
        }

        public string Encrypt(string message, int size)
        {
            try
            {
                string[] file = File.ReadAllLines(@"Keys/public.txt"); // open file, read and close
                int n = int.Parse(file[0]);
                int e = int.Parse(file[1]);

                List<int> encryptedData = new List<int>();
                int encryptedMessage = -1;

                if (message.Length > 0)
                {
                    encryptedMessage = (int)message[0];
                }

                for (int i = 1; i < message.Length; i++)
                {
                    if (i % size == 0)
                    {
                        encryptedData.Add(encryptedMessage);
                        encryptedMessage = 0;
                    }
                    encryptedMessage = encryptedMessage * 1000 + (int)(message[i]);
                }
                encryptedData.Add(encryptedMessage);

                for (int i = 0; i < encryptedData.Count; i++)
                {
                    encryptedData[i] = (int)Math.Pow(encryptedData[i], e) % n;
                }

                return string.Join(" ", encryptedData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Decrypt()
        {

        }
    }
}
