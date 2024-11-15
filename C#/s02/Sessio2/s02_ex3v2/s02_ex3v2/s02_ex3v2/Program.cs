using System;
using System.Runtime.CompilerServices;

namespace ex3
{
    class universal
    {
        public int temps { get; set; }
        public String action { get; set; }
        public universal(int tempsc, String actionc)
        {
            this.temps = tempsc;
            this.action = actionc;
        }
    }
    class main
    {
         static void uni (Object act)
        {
            universal obj = (universal)act;
            System.Console.WriteLine( "START " + obj.action);
            Thread.Sleep(obj.temps);
            System.Console.WriteLine("END" + obj.action);
        }
        

        public static void Main(string[] args)
        {
            universal desu = new universal(1000," DESAYUNAR");
            universal manu = new universal(1000, " UNTAR MANTEQUILLA");
            universal cafeLu = new universal(5000, " PREPARAR CAFE CON LECHE");
            universal cafeu = new universal(1000, " preparar cafe");
            universal lecheu = new universal(1000, " leche");
            universal tostadau = new universal(1000, " preparar tostadas");

            Thread des = new Thread(uni);
            Thread man = new Thread(uni);
            Thread cafeL = new Thread(uni);
            Thread cafe = new Thread(uni);
            Thread leche = new Thread(uni);
            Thread tostada = new Thread(uni);

            cafe.Start(cafeu);
            leche.Start(lecheu);
            tostada.Start(tostadau);
            cafe.Join();
            leche.Join();
            tostada.Join();
            cafeL.Start(cafeLu);
            man.Start(manu);
            cafeL.Join();
            man.Join();
            des.Start(desu);
            des.Join();

        }
    }
}