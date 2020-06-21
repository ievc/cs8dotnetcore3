using System;
using static System.Console;

namespace IterationStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            // while
            int x = 0;
            while(x<10)
            {
                WriteLine(x);   
                x++;
            }

            // do ... while
            /* string password = string.Empty;
            do
            {
                Write("Enter your password: ");
                password = ReadLine();

            }
            while(password !="123");
            WriteLine("Correct!"); */

            // for 
            for(int y=1; y<10; y++)
            {
                WriteLine(y);
            }

            // foreach
            string[] names = {"Adam", "Eugene", "Sonya"};
            foreach(string item in names)
            {
                WriteLine($"{item} has {item.Length} characters.");
            }

            

        }
    }
}
