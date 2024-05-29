using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace ManagementSystem
{
    class Program
    {
            static void Main(string[] args)
            {
                new Manager();

            Console.WriteLine("\nPress <Enter> to close the program!");
            Console.WriteLine("Goodbye!");
            Console.ReadLine();
            }
    }
}