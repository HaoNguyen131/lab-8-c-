using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<int> numbers = new List<int>();

    static void Thread1Function()
    {
        for (int i = 0; i < 100; i++)
        {
            int randomNumber = new Random().Next(1, 11);
            numbers.Add(randomNumber);
            Console.WriteLine($"Thread 1: Generated random number {randomNumber}");
            Thread.Sleep(2000); // sleep for 2 seconds
        }
    }

    static void Thread2Function()
    {
        for (int i = 0; i < 100; i++)
        {
            if (numbers.Count > 0)
            {
                int number = numbers[0];
                numbers.RemoveAt(0);
                double squareRoot = Math.Sqrt(number);
                Console.WriteLine($"Thread 2: Square root of {number} is {squareRoot}");
            }
            Thread.Sleep(1000); // sleep for 1 second
        }
    }

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(Thread1Function);
        Thread thread2 = new Thread(Thread2Function);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("All threads finished");
    }
}