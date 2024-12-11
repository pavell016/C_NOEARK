using System;
using System.Threading;

namespace Trens
{
    class Program
    {
        static int KMT1 = 0; // Posición del Tren 1
        static int KMT2 = 100; // Posición del Tren 2
        static readonly SemaphoreSlim Sem = new SemaphoreSlim(1); // Semáforo para sincronización
        static bool Tren2EnTramoPeligroso = false; // Estado para evitar mensajes repetidos

        static void Main(string[] args)
        {
            // Crear hilos para Tren1 y Tren2.
            Thread TTren1 = new Thread(Tren1);
            Thread TTren2 = new Thread(Tren2);

            TTren1.Start();
            TTren2.Start();

            Console.ReadLine();
        }

        static void Tren1()
        {
            for (int i = 0; i <= 100; i++)
            {
                KMT1 = i;

                // entrada tram perillos
                if (KMT1 == 20)
                {
                    Console.WriteLine("Tren 1 ha entrat al tram perillós (20-60).");
                    Sem.Wait(); 
                }

                // sortida tram perillos
                if (KMT1 == 61)
                {
                    Console.WriteLine("Tren 1 ha sortit del tram perillós.");
                    Sem.Release(); 
                }

                Console.WriteLine("Tren 1({0})", i);

                Thread.Sleep(100); 
            }
        }

        static void Tren2()
        {
            for (int i = 100; i >= 0; i--)
            {
                Sem.Wait(); // Espera si está bloqueado por el Tren 1
                KMT2 = i;

                // entrada tram perillos (s'asegura de que el missatge no es repeteixi
                if (KMT2 >= 20 && KMT2 <= 60 && !Tren2EnTramoPeligroso)
                {
                    Console.WriteLine("\t\t\t\tTren 2 ha entrat al tram perillós (20-60).");
                    Tren2EnTramoPeligroso = true;
                }

                // sortida tram perillos (s'asegura de que el missatge no es repeteixi
                if ((KMT2 < 20 || KMT2 > 60) && Tren2EnTramoPeligroso)
                {
                    Console.WriteLine("\t\t\t\tTren 2 ha sortit del tram perillós.");
                    Tren2EnTramoPeligroso = false;
                }

                Console.WriteLine("\t\t\t\tTren 2({0})", i);

                Sem.Release();

                Thread.Sleep(110); 
            }
        }
    }
}
