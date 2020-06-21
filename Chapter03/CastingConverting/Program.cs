using System;
using static System.Console;
using static System.Convert;

namespace CastingConverting
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            double b = a;
            Console.WriteLine(b);

            double c = 9.8;
            int d = (int)c;
            Console.WriteLine(d);

            long e = 10;
            int f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");

            e = long.MaxValue;
            f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");

            double[] doubles = new[] {9.49, 9.5, 10.49, 10.5, 10.51};
            foreach(double n in doubles)
            {
                WriteLine($"To int({n}) is {ToInt32(n)}");
            }

            foreach(double n in doubles)
            {
                WriteLine(format:
                    "Math.Round({0}, 0, MidpointRounding.AwayFromZero) is {1}",
                    arg0:n,
                    arg1: Math.Round(value:n, digits:0, mode:MidpointRounding.AwayFromZero)
                );
            }

            WriteLine();
            WriteLine("----- Binary converting ---");

            // allocate array of 128 bytes
            byte[] binaryObject = new byte[128];
            // populate array with random bytes
            (new Random()).NextBytes(binaryObject);

            WriteLine("Binary Object as bytes:");
            for (int index = 0; index < binaryObject.Length; index++)
            {
                Write($"{binaryObject[index]:X}");
            }
            WriteLine();
            WriteLine();
            
            // convert to Base64 string and output as text
            string encoded = Convert.ToBase64String(binaryObject);
            WriteLine($"Binary Object as Base64: {encoded}");

            WriteLine();
            WriteLine("String parsing");           

            int age = int.Parse("27");
            DateTime birthday = DateTime.Parse("4 July 1980");
            WriteLine($"I was born {age} years ago");
            WriteLine($"My birthday is {birthday}");
            WriteLine($"My birthday is {birthday:D}");


            WriteLine();
            WriteLine("Using try parse method");           

            Write("How many eggs are there: ");
            int count;
            string input = ReadLine();
            if(int.TryParse(input, out count))
            {
                WriteLine($"There are {count} eggs.");
            }
            else
            {
                WriteLine("I couldn't parse the input.");
            }

            
            WriteLine();
            WriteLine("Using try...catch");           

            WriteLine("Before parsing");
            Write("What is your age? ");
            string input2 = ReadLine();
            try
            {
                int age2 = int.Parse(input2);
                WriteLine($"You are {age2} years old.");
            }
            catch (Exception ex)
            {
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            WriteLine("After parsing");
         

        }
    }
}
