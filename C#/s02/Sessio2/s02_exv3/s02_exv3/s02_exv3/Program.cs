using System;
using System.Threading;
namespace Sessio2
{
    
    class sessio2
    {
        static AutoResetEvent  relevo = new AutoResetEvent(false);
        static Random rnd1 = new Random();
        static void corredor(object id, object nom)
        {
            int idc = (int)id;
            String nomc = (String)nom;
            Console.WriteLine("Corrent [" + idc + "]");
            relevo.WaitOne();
            //La variable temps representa el quanta estona estarpa corrent el corredor,
            //que serà un valor entre 10 i 15 segons.
            int temps;
            temps = rnd1.Next(1000, 1500);
            Thread.Sleep(temps);


            Console.WriteLine("Acabat [" + nomc + "] [temps trigat: " + temps + " milisegons ]");
            relevo.Set();
        }
        static void Main(String[] args)
        {


            Thread Tcorredor1 = new Thread(() => corredor(1, "Corredor 1"));
            Thread Tcorredor2 = new Thread(() => corredor(2, "Corredor 2"));
            Thread Tcorredor3 = new Thread(() => corredor(3, "Corredor 3"));
            Thread Tcorredor4 = new Thread(() => corredor(4, "Corredor 4"));
            Thread Tcorredor5 = new Thread(() => corredor(5, "Corredor 5"));

            Tcorredor1.Start();
            relevo.Set();
            Tcorredor1.Join();
            
            Tcorredor2.Start();
            relevo.Set();
            Tcorredor2.Join();

            Tcorredor3.Start();
            relevo.Set();
            Tcorredor3.Join();

            Tcorredor4.Start();
            relevo.Set();
            Tcorredor4.Join();

            Tcorredor5.Start();
            relevo.Set();
            Tcorredor5.Join();

        }
    }
}

