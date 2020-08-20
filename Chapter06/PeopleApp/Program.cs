using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        private static void Harry_Shout(object sender, EventArgs e)
        {
            Person p = (Person) sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}");
        }

        static void Main(string[] args)
        {
            var harry = new Person {Name = "Harry"};
            var mary = new Person {Name = "Mary"};
            var jill = new Person {Name = "Jill"};

            // call instance method
            var baby1 = mary.ProcreateWith(harry);

            // call static method
            var baby2 = Person.Procreate(harry, jill);

            WriteLine($"{harry.Name} nas {harry.Children.Count} children");
            WriteLine($"{mary.Name} nas {mary.Children.Count} children");
            WriteLine($"{jill.Name} nas {jill.Children.Count} children");

            WriteLine(
                format: "{0}'s first child is named \"{1}\".",
                arg0: harry.Name,
                arg1: harry.Children[0].Name);

            // call an operator
            var baby3 = harry * mary;


            WriteLine($"5! is {Person.Factorial(5)}");

            // working with events
            WriteLine();
            WriteLine("Working with events");

            harry.Shout += Harry_Shout;
            harry.Poke();
            harry.Poke();
            harry.Poke();
            harry.Poke();

            // working with interfaces
            WriteLine();
            WriteLine("Working with interfaces");

 
            Person[] people =
            {
                new Person { Name = "Simon" },
                new Person { Name = "Jenny" },
                new Person { Name = "Adam" },
                new Person { Name = "Richard" }
            };
           
            WriteLine("Initial list of people:");
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }
            
            WriteLine("Use Person's IComparable implementation to sort:");
            Array.Sort(people);
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }

            WriteLine();
            WriteLine("Use PersonComparer's IComparer implementation to sort:");
            
            Array.Sort(people, new PersonComparer());
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }


            WriteLine();
            WriteLine("Reusable types");

            var t1 = new Thing();
            t1.Data = 42;
            WriteLine($"Thing with an integer: {t1.Process(42)}");
            var t2 = new Thing();
            t2.Data = "apple";
            WriteLine($"Thing with a string: {t2.Process("apple")}");

            WriteLine();
            WriteLine("Types reusable with generics");

            var gt1 = new GenericThing<int>();
            gt1.Data = 42;
            var gt2 = new GenericThing<string>();
            gt2.Data = "apple";
            WriteLine($"GenericThing with a string: {gt2.Process("apple")}");

            WriteLine();
            WriteLine("Using squarer");

            string number1 = "4";
            WriteLine("{0} squarer is {1}",
                arg0: number1,
                arg1: Squarer.Square<string>(number1)
            );

            byte number2 = 3;
            WriteLine("{0} squared is {1}",
                arg0: number2,
                arg1: Squarer.Square(number2)
            );

            WriteLine();
            WriteLine("Test struct");

            var dv1 = new DisplacementVector(3,5);
            var dv2 = new DisplacementVector(-2,7);
            var dv3 = dv1 + dv2;
            WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})");


            WriteLine();
            WriteLine("Inheriting from classes");
            Employee john = new Employee
            {
                Name = "John Jones",
                DateOfBirth = new DateTime(1990, 7, 28)
            };
            john.WriteToConsole();

            john.EmployeeCode = "JJ001";
            john.HireDate = new DateTime(2014, 11, 23);
            WriteLine($"{john.Name} was hired on {john.HireDate:dd/MM/yy}");

            WriteLine();
            WriteLine("Overriding members");
            WriteLine(john.ToString());

            WriteLine();
            WriteLine("Understanding polymorphism");

            Employee aliceInEmployee = new Employee{ Name = "Alice", EmployeeCode = "AA123" };
            Person aliceInPerson = aliceInEmployee;
            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();
            WriteLine(aliceInEmployee.ToString());
            WriteLine(aliceInPerson.ToString());
            
            WriteLine();
            WriteLine("Explicit casting");
            if (aliceInPerson is Employee)
            {
                WriteLine($"{nameof(aliceInPerson)} IS an Employee");
                Employee explicitAlice = (Employee)aliceInPerson;
                // safely do something with explicitAlice
            }

            Employee aliceAsEmployee = aliceInPerson as Employee;
            if (aliceAsEmployee != null)
            {
                WriteLine($"{nameof(aliceInPerson)} AS an Employee");
                // do something with aliceAsEmployee
            }


            WriteLine();
            WriteLine("Inheriting exeption");
            try
            {
                aliceAsEmployee.TimeTravel(new DateTime(1999, 12, 31));
                aliceAsEmployee.TimeTravel(new DateTime(1950, 12, 25));
            }
                catch (PersonException ex)
            {
                WriteLine(ex.Message);
            }

            WriteLine("---------------------");
            WriteLine("Extention methods");

            string email1 = "pamela@test.com";
            string email2 = "ian&test.com";
            WriteLine( "{0} is a valid e-mail address: {1}",
                arg0: email1,
                arg1: StringExtensions.IsValidEmail(email1));
            WriteLine("{0} is a valid e-mail address: {1}",
                arg0: email2,
                arg1: StringExtensions.IsValidEmail(email2));
            
            WriteLine("{0} is a valid e-mail address: {1}",
                arg0: email1,
                arg1: email1.IsValidEmail());
            WriteLine("{0} is a valid e-mail address: {1}",
                arg0: email2,
                arg1: email2.IsValidEmail());

        }
    }
}
