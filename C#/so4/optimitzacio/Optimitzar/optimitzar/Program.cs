using System;
using System.Threading;

namespace Optimitzar
{
    class Program
    {
        static int[] BaseDatos = new int[100];
        static int[] BaseDatosLocal = new int[100];
        static int[] BaseDatosLocalOpt = new int[100];
        static readonly object locker = new object();

        static void Main(string[] args)
        {
            int SumaTotal = 0;
            DateTime T1;
            DateTime T2;

            Inicializar();

            //**********************TAREA 1: Suma************************************
            T1 = DateTime.Now;

            //for (int i = 0; i < 100; i++)
            //{
            //    SumaTotal = SumaTotal + ConsultaDB(i);
            //}
            Parallel.For(0, 100, delegate (int i) {
                lock (locker)
                {
                    SumaTotal = SumaTotal + ConsultaDB(i);
                }
            });
            T2 = DateTime.Now;
            TimeSpan Ttotal = new TimeSpan(T2.Ticks - T1.Ticks);

            Console.WriteLine("Total suma es {0}, y ha tardado {1}", SumaTotal, Ttotal.ToString());


            //******************************************* TAREA 2: Copia datos
            T1 = DateTime.Now;

            //for (int i = 0; i < 100; i++)
            //{
            //    BaseDatosLocal[i] = ConsultaDB(i);
            //}
            Parallel.For(0, 100, delegate (int i) {
                BaseDatosLocal[i] = ConsultaDB(i);
            });
            MuestraValores(BaseDatosLocal);
            T2 = DateTime.Now;
            Ttotal = new TimeSpan(T2.Ticks - T1.Ticks);

            Console.WriteLine("Total copiar ha tardado {0}", Ttotal.ToString());


            //********************************************** TAREA 3: Serializar números
            T1 = DateTime.Now;
            string[] resultados = new string[100];

            Parallel.For(0, 100, i =>
            {
                lock (locker) { 
                    resultados[i] = i + ":" + ConsultaDB(i) + ";"; // Cada hilo guarda en su índice correspondiente
                }
            });

            // Concatenación final en orden
            string serial = string.Join("", resultados);

            Console.WriteLine("Serialización ordenada:");
            Console.WriteLine(serial);

            T2 = DateTime.Now;
            Ttotal = new TimeSpan(T2.Ticks - T1.Ticks);
            Console.WriteLine("Total copiar ha tardado {0}", Ttotal.ToString());
        }

        static void Inicializar()
        {
            Random rnd = new Random();

            //for (int i = 0; i < 100; i++)
            //{
            //    BaseDatos[i] = rnd.Next(0, 100);
            //}

            Parallel.For(0, 100, delegate (int i) {
                BaseDatos[i] = rnd.Next(0, 100);
            });
        }

        static void MuestraValores(int[] dades)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.Write(" {0},", dades[i].ToString());
            //}

            Parallel.For(0, 100, delegate (int i) {
                Console.Write(" {0},", dades[i].ToString());
            });
        }

        static int ConsultaDB(int index)
        {
            Thread.Sleep(100);
            return BaseDatos[index];
        }
    }
}
