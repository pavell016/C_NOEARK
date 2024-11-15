using System;

namespace Session1
{
    class S01
    {
        static void Go()
        {
            for (int i = 0; i <= 50; i++)
            {
                Console.Write(i);
                Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            Console.WriteLine("si");
            Thread T = new Thread(Go);
            T.Start();
            Go();
            Console.WriteLine("Sa acabat");
        }
    }
}
