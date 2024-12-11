using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Trens
{
    class Program
    {

        static void Main(string[] args)
        {            
            Thread TTren1 = new Thread(Tren1);
            Thread TTren2 = new Thread(Tren2);

            TTren1.Start();
            TTren2.Start();
            Console.ReadLine();
        }

        static void Tren1()
        {
            for (int i=0;i<=100; i++)
            {                
                if ((i>=20) && (i<=60))
                    Console.WriteLine("\t\tTren 1({0})",i);
                else
                    Console.WriteLine("Tren 1({0})", i);

                Thread.Sleep(100);
            }
        }

        static void Tren2()
        {
            for (int i=100; i>=0; i--)
            {
                if ((i >= 20) && (i <= 60))
                    Console.WriteLine("\t\tTren 2({0})", i);
                else
                    Console.WriteLine("\t\t\t\tTren 2({0})", i);

                Thread.Sleep(110);
            }
        }
    }
}
