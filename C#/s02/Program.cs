using System;
using System.Threading;
namespace Sessio2
{
    class Corredor
    {
        public Corredor(int id, string nom) {
            idc = id;
            nomc = nom;
        }
        public int idc { get; set; }
        public String nomc {  get; set; }
    }
    class sessio2
    {
        static Random rnd1 = new Random();
        static void corredor(Object parameters)
        {
            Corredor corredorobj = (Corredor) parameters;
            Console.WriteLine("Corrent [" + corredorobj.idc + "]");

            //La variable temps representa el quanta estona estarpa corrent el corredor,
            //que serà un valor entre 10 i 15 segons.
            int temps;
            temps = rnd1.Next(1000, 1500);
            Thread.Sleep(temps);
            

            Console.WriteLine("Acabat [" + corredorobj.nomc + "] [temps trigat: "+ temps +" milisegons ]");
        }
        static void Main(String[] args)
        {
            Corredor corredor1 = new Corredor(11, "Fernando De La Mancha");
            Corredor corredor2 = new Corredor(21, "Fernando De La Mancha");
            Corredor corredor3 = new Corredor(31, "Fernando De La Mancha");
            Corredor corredor4 = new Corredor(41, "Fernando De La Mancha");
            Corredor corredor5 = new Corredor(51, "Fernando De La Mancha");

            Thread Tcorredor1 = new Thread(corredor);
            Thread Tcorredor2 = new Thread(corredor);
            Thread Tcorredor3 = new Thread(corredor);
            Thread Tcorredor4 = new Thread(corredor);
            Thread Tcorredor5 = new Thread(corredor);

            Tcorredor1.Start(corredor1);
            Tcorredor2.Start(corredor2);
            Tcorredor3.Start(corredor3);
            Tcorredor4.Start(corredor4);
            Tcorredor5.Start(corredor5);

        }
    }
}
