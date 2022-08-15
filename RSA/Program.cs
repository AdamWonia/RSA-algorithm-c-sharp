using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA rsa = new RSA();
            // Create new keys:
            rsa.CreateKeys();
            string encryptedMessage = rsa.Encrypt("test", 2);
            string decryptedMessage = rsa.Decrypt(encryptedMessage, 2);
            Console.WriteLine(decryptedMessage);
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
                int floorQuotient = a / b;
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
                string[] file = File.ReadAllLines(@"Keys\public.txt"); // open file, read and close
                long n = long.Parse(file[0]);
                long e = long.Parse(file[1]);

                List<long> encryptedData = new List<long>();
                long encryptedMessage = -1;

                if (message.Length > 0)
                {
                    encryptedMessage = message[0];
                }

                for (int i = 1; i < message.Length; i++)
                {
                    if (i % size == 0)
                    {
                        encryptedData.Add(encryptedMessage);
                        encryptedMessage = 0;
                    }
                    encryptedMessage = encryptedMessage * 1000 + (message[i]);
                }
                encryptedData.Add(encryptedMessage); //[101116, 115116]
                for (int i = 0; i < encryptedData.Count; i++)
                {
                    encryptedData[i] = PowerAndModulo(encryptedData[i], e, n);
                }

                return string.Join(" ", encryptedData); // "80 1"
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Decrypt(string DataBlock, int size)
        {
            string[] file = File.ReadAllLines(@"Keys/private.txt");
            int n = int.Parse(file[0]);
            int d = int.Parse(file[1]);

            string[] DataBlockList = DataBlock.Split(" ");
            List<int> DataBlockInt = new List<int>();

            foreach (string item in DataBlockList)
            {
                DataBlockInt.Add(int.Parse(item));
            }

            string message = string.Empty;

            for (int i = 0; i < DataBlockInt.Count; i++)
            {
                DataBlockInt[i] = (int)Math.Pow(DataBlockInt[i], d) % n;
                string x = string.Empty;
                for (int j = 0; j < size; j++)
                {
                    x = (char)(DataBlockInt[j] % 1000) + x;
                    DataBlockInt[j] /= 1000; // int_blok_danych[i] //= 1000
                }
                message += x;
            }

            return message;
        }

        public void CreateKeys()
        {
            // Take random number from 1 to 50
            Random rand = new Random();
            int liczba1 = rand.Next(0, 10);
            int liczba2 = rand.Next(0, 10);

            string[] file = File.ReadAllLines("Prime Numbers/primeNumbers.txt");
            int p = int.Parse(file[liczba1]);
            int q = int.Parse(file[liczba2]);

            //int p = 13;
            //int q = 23;

            int fi = (p - 1) * (q - 1);
            int e = CountE(fi);
            //int e = 103;
            long n = p * q;

            // tu jest syf
            var result = ExtendedEuclideanAlgorithm(e, fi);
            int NWD = result.a;
            int x = result.xPrev;
            int y = result.yPrev;

            int d;
            if (x < 0)
            {
                d = fi + x;
            }
            else
            {
                d = x;
            }
            // Public key: 
            File.WriteAllLines(@"Keys/public.txt", new string[] { n.ToString(), e.ToString() });
            // Private key"
            File.WriteAllLines(@"Keys/private.txt", new string[] { n.ToString(), d.ToString() });
        }

        public static long PowerAndModulo(long x, long e, long m)
        {
            long result = 1;
            x = x % m;
            while (e > 0)
            {
                if (e % 2 == 1)
                {
                    result = (result * x) % m;
                    e -= 1;
                }
                x = (x * x) % m;
                e /= 2;
            }
            return result;
        }
    }
}
