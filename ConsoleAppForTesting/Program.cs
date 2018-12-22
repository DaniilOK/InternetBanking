using System;
using IB.Common.Helpers;

namespace ConsoleAppForTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            int pin = 1111;
            var hash = CalculateHashHelper.ComputeHash(pin.ToString());

            Console.WriteLine(hash.Length);
            Console.WriteLine(hash);

        }
    }
}
