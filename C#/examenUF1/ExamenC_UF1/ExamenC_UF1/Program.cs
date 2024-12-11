using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex1
{
    class Program
    {
        static public double TotalSuma = 0;
        static public double TotalMultiplica = 1;
        static public double TotalParells = 0;
        static public double TotalSenars = 0;
        static readonly object locker = new object();
        public static int[] Valors = new int[100];

        static void Main(string[] args)
        {
            int i = 0;

            DateTime T1, T2;

            // Inicialització de valors
            iniciar();

            T1 = DateTime.Now;

            // Mostra els números generats (no és important l'ordre)
            Console.WriteLine("Mostrant...");
            Parallel.For(0, 100, delegate (int i)
            {
                Console.WriteLine(Valors[i]);
                Thread.Sleep(50);
            });
            

            // Càlcul de la suma
            Console.WriteLine("calculant...");
            Thread S = new Thread(suma);
            S.Start();


            // Càlcul de la multiplicació
            Thread M = new Thread(mult);
            M.Start();


            // Contatge de parells i senars
            Thread PS = new Thread(comp);
            PS.Start();
            
            PS.Join();
            T2 = DateTime.Now;
            TimeSpan Ttotal_NoThread = new TimeSpan(T2.Ticks - T1.Ticks);
            Console.WriteLine("Total suma: {0}, total multiplica: {1}, total senars: {2}, total parells: {3}, total temps {4}", TotalSuma.ToString(), TotalMultiplica.ToString(), TotalSenars.ToString(), TotalParells.ToString(), Ttotal_NoThread.ToString());

            Console.Read();
        }

        static public void iniciar()
        {
            Random random = new Random();
            Parallel.For(0, 100, i =>
            {
                Valors[i] = random.Next(1, 10);
            });
        }
        static public void suma()
        {
            for (int i = 0; i < Valors.Length; i++)
            {
                TotalSuma = TotalSuma + Valors[i];
            }
        }

        static public void mult()
        {
            for (int i = 0; i < Valors.Length; i++)
            {
                TotalMultiplica = TotalMultiplica * Valors[i];
            }
        }
        static public void comp()
        {
            for (int i = 0; i < 100; i++)
            {
                if ((Valors[i] % 2) == 0) {
                    lock (locker)
                    {
                        TotalParells++;
                    }
                }
                else
                {
                    lock (locker)
                    {
                        TotalSenars++;
                    }
                }
                
                Thread.Sleep(50);
            }
        }
    }
}

