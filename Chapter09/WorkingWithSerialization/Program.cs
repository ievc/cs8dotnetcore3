﻿using System; // DateTime
using System.Collections.Generic; // List<T>, HashSet<T>
using System.Xml.Serialization; // XmlSerializer
using System.IO; // FileStream
using Packt.Shared; // Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;

namespace WorkingWithSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            // create an object graph
            var people = new List<Person>
            {
                new Person(30000M) { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1974, 3, 14) },
                new Person(40000M) { FirstName = "Bob", LastName = "Jones", DateOfBirth = new DateTime(1969, 11, 23) },
                new Person(20000M) { FirstName = "Charlie",LastName = "Cox", DateOfBirth = new DateTime(1984, 5, 4),
                 Children = new HashSet<Person>
                    { new Person(0M) { FirstName = "Sally", LastName = "Cox", DateOfBirth = new DateTime(2000, 7, 12) } } 
                }
            };
            
            // create object that will format a List of Persons as XML
            var xs = new XmlSerializer(typeof(List<Person>));
            
            // create a file to write to
            string path = Combine(CurrentDirectory, "people.xml");
            using (FileStream stream = File.Create(path))
            {
                // serialize the object graph to the stream
                xs.Serialize(stream, people);
            }
            WriteLine("Written {0:N0} bytes of XML to {1}", arg0: new FileInfo(path).Length, arg1: path);
            WriteLine();
            
            // Display the serialized object graph
            WriteLine(File.ReadAllText(path));


            // serialize to json
            var jsonFilePath = Combine(CurrentDirectory, "people.json");
            using(StreamWriter jsonStream = File.CreateText(jsonFilePath))
            {
                // create an object that will format as JSON
                var jss = new Newtonsoft.Json.JsonSerializer();
                jss.Serialize(jsonStream, people);

                WriteLine();WriteLine("Written {0:N0} bytes of JSON to: {1}",
                    arg0: new FileInfo(jsonFilePath).Length,
                    arg1: jsonFilePath);
                // Display the serialized object graph
                WriteLine(File.ReadAllText(jsonFilePath));
            }
        }
    }
}
