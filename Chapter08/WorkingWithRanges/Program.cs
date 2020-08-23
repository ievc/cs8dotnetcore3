using System;
using static System.Console;

namespace WorkingWithRanges
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Samantha Jones";
            int indexOfSpace = name.IndexOf(' ');
            string firstName = name.Substring(startIndex: 0, length: indexOfSpace);
            string lastName = name.Substring(startIndex: name.Length - (name.Length - indexOfSpace - 1),
                length: name.Length - indexOfSpace - 1);
            
            WriteLine($"First name: {firstName}, Last name: {lastName}");
            ReadOnlySpan<char> nameAsSpan = name.AsSpan();
            var firstNameSpan = nameAsSpan[0..firstName.Length];
            var lastNameSpan = nameAsSpan[^lastName.Length..^0];
            WriteLine("First name: {0}, Last name: {1}",
            arg0: firstNameSpan.ToString(),
            arg1: lastNameSpan.ToString());
        }
    }
}
