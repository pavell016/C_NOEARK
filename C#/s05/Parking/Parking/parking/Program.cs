using System;
using System.Threading;

namespace Parking
{
    class Program
    {
        static int ThreadCount = 0;
        static readonly object locker = new object();
        static SemaphoreSlim sem = new SemaphoreSlim(5);
        static Boolean parking = true;
        
        
        static void Main(string[] args)
        {
            Thread I = new Thread(is_pressed);
            I.Start();
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
            sem.Wait();
            if (parking)
            {
                lock (locker)
                {
                    ThreadCount++;
                    Console.WriteLine("                                         hay {0} coches dentro del parking", ThreadCount);
                }
                Console.WriteLine("             Coche {0} está dentro del párking", IdCoche);
                Thread.Sleep(TiempoParking);
                Console.WriteLine("                         Coche {0} está fuera del párking", IdCoche);
                sem.Release();
                lock (locker)
                {
                    ThreadCount--;
                    Console.WriteLine("                                         hay {0} coches dentro del parking", ThreadCount);
                }
            }
            else {
                Console.WriteLine("Coche {0} pasa de largo", IdCoche);
                sem.Release();
            }
        }
        static void is_pressed() {
            while (parking) {
                if (Console.KeyAvailable) {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.T) {
                        
                        parking = false;
                        Console.WriteLine("Parking Cerrado");
                        
                    }
                }
            }
            
        }
    }

}
