using System;
using System.Threading;
namespace ExLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread T = new Thread(() => Escribe_Lambda(i));
                T.Start();
            }

            Console.ReadLine();
        }

        static void Escribe_Lambda(int valor)
        {
            Console.WriteLine("Valor: {0}", valor.ToString());
        }
    }
}
