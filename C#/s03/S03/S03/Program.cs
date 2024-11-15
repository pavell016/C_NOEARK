using System;
namespace Program
{
    class Program
    {
        static int contador = 0;
        static readonly object locker = new object();

        static void contar() {
                for (int i = 0; i < 500; i++) { 
                    lock (locker) { 
                        contador++;
                    
                    }
                }
            
        }
        
        static void Main(string[]args)
        {

            
            for (int i = 0; i < 5; i++) { 
                Thread p1 = new Thread(contar);
                p1.Start();
                Console.WriteLine(contador);
            }
            Thread.Sleep(10000);


            Console.WriteLine("contado hasta" + contador);

        }
    }
}