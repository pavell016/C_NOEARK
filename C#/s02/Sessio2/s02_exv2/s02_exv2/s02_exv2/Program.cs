using System;
using System.Threading;
namespace Sessio2
{
    class Corredor
    {
        public Corredor(int id, string nom)
        {
            idc = id;
            nomc = nom;
        }
        public int idc { get; set; }
        public String nomc { get; set; }
    }
    class sessio2
    {
        static Random rnd1 = new Random();
        static void corredor(object id, object nom)
        {
            int idc =(int)id;
            String nomc =(String)nom;
            Console.WriteLine("Corrent [" + idc + "]");

            //La variable temps representa el quanta estona estarpa corrent el corredor,
            //que serà un valor entre 10 i 15 segons.
            int temps;
            temps = rnd1.Next(1000, 1500);
            Thread.Sleep(temps);


            Console.WriteLine("Acabat [" + nomc + "] [temps trigat: " + temps + " milisegons ]");
        }
        static void Main(String[] args)
        {
            

            Thread Tcorredor1 = new Thread(() => corredor(11, "Ferran"));
            Thread Tcorredor2 = new Thread(() => corredor(69, "Pau"));
            Thread Tcorredor3 = new Thread(() => corredor(77, "Francisco"));
            Thread Tcorredor4 = new Thread(() => corredor(11, "The cooler Ferran"));
            Thread Tcorredor5 = new Thread(() => corredor(11, "The even cooler Ferran"));

            Tcorredor1.Start();
            Tcorredor2.Start();
            Tcorredor3.Start();
            Tcorredor4.Start();
            Tcorredor5.Start();

        }
    }
}
