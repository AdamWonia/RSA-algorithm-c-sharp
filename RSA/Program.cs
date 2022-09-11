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
            string publicKeyPath = @"Keys\public.txt";
            string privateKeyPath = @"Keys\private.txt";
            string primeNumberPath = @"Prime Numbers/primeNumbers.txt";

            Console.WriteLine("Do you want to create a new public and private keys? Choose Y or N:");
            string answer = Console.ReadLine();
            if (answer.ToLower().Equals("y"))
                rsa.CreateKeys(primeNumberPath);
            else if (answer.ToLower().Equals("n"))
            {
                Console.WriteLine("Insert a message you want to encrypt: ");
                string encyptedMessage = rsa.Encrypt(Console.ReadLine(), publicKeyPath);
                Console.WriteLine("Encrypted message is:\n{0}", encyptedMessage);
                Console.WriteLine("Decrypting message...");
                Console.WriteLine(rsa.Decrypt(encyptedMessage, privateKeyPath));
            }
            else
                Console.WriteLine("Input value is incorrect");
        }
    }

    public class RSA
    {
        private int GreatestCommonDivisor(int firstNumber, int secondNumber)
        {
            while (firstNumber != secondNumber)
            {
                if (firstNumber > secondNumber)
                    firstNumber = firstNumber - secondNumber;
                else
                    secondNumber = secondNumber - firstNumber;
            }
            return firstNumber;
        }

        private static long PowerAndModulo(long @base, long exp, long mod)
        {
            long result = 1;
            @base = @base % mod;
            while (exp > 0)
            {
                if (exp % 2 == 1)
                {
                    result = (result * @base) % mod;
                    exp -= 1;
                }
                @base = (@base * @base) % mod;
                exp /= 2;
            }
            return result;
        }

        private int ExtendedEuclideanAlgorithm(int a, int b)
        {
            (int x, int xPrev, int y, int yPrev) = (0, 1, 1, 0);
            while (b != 0)
            {
                int floorQuotient = a / b;
                (a, b) = (b, a - b * floorQuotient);
                (xPrev, x) = (x, xPrev - x * floorQuotient);
                (yPrev, y) = (y, yPrev - y * floorQuotient);
            }
            return xPrev;
        }

        private int CountE(int phi)
        {
            Random rand = new Random();
            while (true)
            {
                int e = rand.Next(2, phi);
                if (GreatestCommonDivisor(e, phi) == 1)
                    return e;
            }
        }

        public string Encrypt(string message, string publicKeyPath)
        {
            try
            {
                string[] file = File.ReadAllLines(publicKeyPath); // open file, read and close
                long n = long.Parse(file[0]);
                long e = long.Parse(file[1]);
                long encryptedMessage = -1;

                List<long> encryptedData = new List<long>();

                if (message.Length > 0)
                    encryptedMessage = message[0];

                for (int i = 1; i < message.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        encryptedData.Add(encryptedMessage);
                        encryptedMessage = 0;
                    }
                    encryptedMessage = encryptedMessage * 1000 + (message[i]);
                }
                encryptedData.Add(encryptedMessage);
                for (int i = 0; i < encryptedData.Count; i++)
                    encryptedData[i] = PowerAndModulo(encryptedData[i], e, n);

                return string.Join(" ", encryptedData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Decrypt(string DataBlock, string privateKeyPath)
        {
            string[] file = File.ReadAllLines(privateKeyPath);
            string[] DataBlockList = DataBlock.Split(" ");
            int n = int.Parse(file[0]);
            int d = int.Parse(file[1]);

            List<long> DataBlockInt = new List<long>();

            foreach (string item in DataBlockList)
                DataBlockInt.Add(int.Parse(item));

            string message = string.Empty;

            for (int i = 0; i < DataBlockInt.Count; i++)
            {
                DataBlockInt[i] = PowerAndModulo(DataBlockInt[i], d, n);
                string x = string.Empty;
                for (int j = 0; j < 2; j++)
                {
                    x = (char)(DataBlockInt[i] % 1000) + x;
                    DataBlockInt[i] /= 1000;
                }
                message += x;
            }
            return message;
        }
        public void CreateKeys(string primeNumbersPath)
        {
            // Take random number from 50 to 200
            Random rand = new Random();
            int liczba1 = rand.Next(50, 200);
            int liczba2 = rand.Next(50, 200);

            string[] file = File.ReadAllLines(primeNumbersPath);
            int p = int.Parse(file[liczba1]);
            int q = int.Parse(file[liczba2]);
            int fi = (p - 1) * (q - 1);
            int e = CountE(fi);
            long n = p * q;
            int x = ExtendedEuclideanAlgorithm(e, fi);
            int d;

            if (x < 0)
                d = fi + x;
            else
                d = x;

            // Create and store a public key: 
            File.WriteAllLines(@"Keys/public.txt", new string[] { n.ToString(), e.ToString() });
            // Create and store a private key:
            File.WriteAllLines(@"Keys/private.txt", new string[] { n.ToString(), d.ToString() });
            Console.WriteLine("Keys has been created and stored in folder \"Keys\"");
        }
    }
}
