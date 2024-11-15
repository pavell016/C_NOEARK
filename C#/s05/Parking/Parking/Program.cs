using System;
using System.Threading;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 25; i++)
            {
                Thread C = new Thread(Coche);
                C.Start(i);
            }
            Console.ReadLine();
        }

        static void Coche(object Id)
        {
            Random rnd = new Random();
            int TiempoAcceso;
            int TiempoParking;
            int IdCoche = Convert.ToInt32(Id);

            TiempoAcceso = rnd.Next(1000, 5000);
            TiempoParking = rnd.Next(1000, 5000);

            Thread.Sleep(TiempoAcceso);

            Console.WriteLine("Coche {0} quiere entrar al párking", IdCoche);
            Console.WriteLine("             Coche {0} está dentro del párking", IdCoche);
            Thread.Sleep(TiempoParking);
            Console.WriteLine("                         Coche {0} está fuera del párking", IdCoche);
        }
    }
}
