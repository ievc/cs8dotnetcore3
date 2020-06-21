using System;
using static System.Console;

namespace CheckingForOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            checked
            {
                try
                {
                    int x = int.MaxValue -1;
                    Console.WriteLine($"Initial value: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");
                    x++;
                    Console.WriteLine($"After incrementing: {x}");

                    //int y = int.MaxValue + 1;
                }
                catch (OverflowException)
                {
                    WriteLine("The code overflowed but I caught the exception.");
                }
            }
        }
    }
}
