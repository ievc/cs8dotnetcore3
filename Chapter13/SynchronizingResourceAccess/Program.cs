using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static Random r = new Random();
        static string message;  // a shared resource

        static object conch = new object();

        static int Counter;

        static void MethodA()
        {
            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    message += "A";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    message += "B";
                    Interlocked.Increment(ref Counter);
                    Write(".");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }


        
        static void Main(string[] args)
        {
            WriteLine("Please wait for the tasks to complete");
            Stopwatch timer = Stopwatch.StartNew();
            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);
            Task.WaitAll(new Task[]{ a, b });
            WriteLine();
            WriteLine($"Results: {message}");
            WriteLine($"{timer.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
            WriteLine($"{Counter} string modifications.");
        }
    }
}
