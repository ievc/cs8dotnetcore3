using System;
using static System.Console;

namespace Formatting
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfApples = 12;
            decimal pricePerApple = 0.35M;
            Console.WriteLine(
                format: "{0} apples cost {1:C}",
                arg0: numberOfApples,
                arg1: pricePerApple * numberOfApples
            );

            Console.WriteLine($"{numberOfApples} apples cost {pricePerApple * numberOfApples:C}");

            string applesText = "Apples";
            int applesCount = 1234;
            string bananasText = "Bananas";
            int bananasCount = 56789;
            Console.WriteLine(
                format: "{0,-8} {1,10:D6}",
                arg0: "Name",
                arg1: "Count");
            Console.WriteLine(
                format: "{0,-8} {1,10:D6}",
                arg0: applesText,
                arg1: applesCount);
            Console.WriteLine(
                format: "{0,-8} {1,10:D6}",
                arg0: bananasText,
                arg1: bananasCount);
            
            decimal cur = 23.59M;
            Console.WriteLine(
                format: "{0,-8} {1,10:C4}",
                arg0: "Decimal",
                arg1: cur);

            /* Write("Press any key combination: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            WriteLine("Key: {0}, Char: {1}, Modifiers: {2}",
                arg0: key.Key,
                arg1: key.KeyChar,
                arg2: key.Modifiers); */

            WriteLine($"There are {args.Length} args");            
            foreach (var arg in args)
            {
                WriteLine(arg);
            }
            
        }
    }
}
