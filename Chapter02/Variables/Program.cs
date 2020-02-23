using System;
using System.IO;
using System.Xml;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            object height = 1.88;
            object name = "Eugene";
            Console.WriteLine($"{name} is {height} metres tall.");

            // int length1 = name.Length;
            int length2 = ((string)name).Length;
            Console.WriteLine($"{name} has {length2} characters");

            // storing a string in a dynamic object
            dynamic anotherName = "Sara";

            // this compiles but would throw an exception at run-time
            // if you later store a data type that does not have a
            // property named Length
            int length = anotherName.Length;

            var population = 66_000_000; // 66 million in UK
            var weight = 1.88; // in kilograms
            var price = 4.99M; // in pounds sterling
            var fruit = "Apples"; // strings use double-quotes
            var letter = 'Z'; // chars use single-quotes
            var happy = true; // Booleans have value of true or false

            /* // good use of var because it avoids the repeated type
            // as shown in the more verbose second statement
            var xml1 = new XmlDocument();
            XmlDocument xml2 = new XmlDocument();
            // bad use of var because we cannot tell the type, so we
            // should use a specific type declaration as shown in
            // the second statement
            var file1 = File.CreateText(@"D:\Temp\something.txt");
            StreamWriter file2 = File.CreateText(@"D:\Temp\something.txt"); */

            Console.WriteLine($"default(int) = {default(int)}");
            Console.WriteLine($"default(bool) = {default(bool)}");
            Console.WriteLine($"default(DateTime) = {default(DateTime)}");
            Console.WriteLine($"default(string) = {default(string)}");

        }
    }
}
