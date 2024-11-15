using System;

namespace ex3
{
    class main
    {
        public static void preparar_cafe()
        {
            Console.WriteLine("preparar Cafe START");
            Thread.Sleep(1000);
            Console.WriteLine("END preparar cafe");
        }
        public static void calentar_leche()
        {
            Console.WriteLine("calentar leche START");
            Thread.Sleep(1000);
            Console.WriteLine("END calentar leche");
        }
        public static void tostadas()
        {
            Console.WriteLine("preparar tostadas START");
            Thread.Sleep(1000);
            Console.WriteLine("END preparar tostadas");
        }
        public static void preparar_cafe_con_leche()
        {
            Console.WriteLine("preparar Cafe con leche START");
            Thread.Sleep(1000);
            Console.WriteLine("END preparar cafe con leche");
        }
        public static void mantequilla()
        {
            Console.WriteLine("untar mantequilla START");
            Thread.Sleep(1000);
            Console.WriteLine("END untar mantequilla");
        }
        public static void desayunar()
        {
            Console.WriteLine("desayunar START");
            Thread.Sleep(1000);
            Console.WriteLine("END desayunar");
        }
        public static void Main(string[] args)
        {

            Thread des = new Thread(desayunar);
            Thread man = new Thread(mantequilla);
            Thread cafeL = new Thread(preparar_cafe_con_leche);
            Thread cafe = new Thread(preparar_cafe);
            Thread leche = new Thread(calentar_leche);
            Thread tostada = new Thread(tostadas);

            cafe.Start();
            leche.Start();
            tostada.Start();
            cafe.Join();
            leche.Join();
            tostada.Join();
            cafeL.Start();
            man.Start();
            cafeL.Join();
            man.Join();
            des.Start();
            des.Join();

        }
    }
}