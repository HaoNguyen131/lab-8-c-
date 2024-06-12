using System;
using System.Diagnostics;
using System.Threading;

public class Deadlock
{
    static object lock1 = new object();
    static object lock2 = new object();

    static void Thread1FunctionLab()
    {
        if (Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(1000)))
        {
            try
            {
                Console.WriteLine("Thread 1 acquired lock 1");
                Thread.Sleep(100);
                Console.WriteLine("Thread 1 is waiting for lock2");

                if (Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(1000)))
                {
                    try
                    {
                        Console.WriteLine("Thread 1 acquired lock 2");
                    }
                    finally
                    {
                        Monitor.Exit(lock2);
                    }
                }
                else
                {
                    Console.WriteLine("Thread 1 failed to acquire lock 2");
                }
            }
            finally
            {
                Monitor.Exit(lock1);
            }
        }
        else
        {
            Console.WriteLine("Thread 1 failed to acquire lock 1");
        }
    }

    static void Thread2FunctionLab()
    {
        if (Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(1000)))
        {
            try
            {
                Console.WriteLine("Thread 2 acquired lock 2");
                Thread.Sleep(100);
                Console.WriteLine("Thread 2 is waiting for lock1");

                if (Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(1000)))
                {
                    try
                    {
                        Console.WriteLine("Thread 2 acquired lock 1");
                    }
                    finally
                    {
                        Monitor.Exit(lock1);
                    }
                }
                else
                {
                    Console.WriteLine("Thread 2 failed to acquire lock 1");
                }
            }
            finally
            {
                Monitor.Exit(lock2);
            }
        }
        else
        {
            Console.WriteLine("Thread 2 failed to acquire lock 2");
        }
    }

    public void bad2()
    {
        Thread thread1 = new Thread(Thread1FunctionLab);
        Thread thread2 = new Thread(Thread2FunctionLab);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("end start");
    }
}