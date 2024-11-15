using System;
using System.Threading;

namespace Atletisme
{
    class Program
    {
        static Random rnd1 = new Random();

        static void Main(string[] args)
        {

        }

        static void corredor()
        {
            Console.WriteLine("Corrent");

            //La variable temps representa el quanta estona estarpa corrent el corredor,
            //que serà un valor entre 10 i 15 segons.
            int temps;
            temps = rnd1.Next(1000, 1500);
            Thread.Sleep(temps);

            Console.WriteLine("Acabat");
        }
    }
}
