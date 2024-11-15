using System;
namespace S01;

class Sessio1
{
    //static void Go()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        Console.Write(i);
    //        Console.Write(" ");
    //        Thread.Sleep(100);
    //    }
    //}

    //static void aa()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        Console.Write("A");
            
    //        Thread.Sleep(1);
    //    }
    //}
    //static void bb()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        Console.Write("b ");
    //        Thread.Sleep(1);
    //    }
    //}

    static void wakeup()
    {
        Console.WriteLine("WAKE UP");
        Thread.Sleep(3000);
        Console.WriteLine("OUT OF BED");
    }
    static void music()
    {
        Console.WriteLine("music on");
        Thread.Sleep(20000);
        Console.WriteLine("music off");
    }
    static void shower()
    {
        Console.WriteLine("start shower");
        Thread.Sleep(15000);
        Console.WriteLine("end shower");
    }
    static void dressup()
    {
        Console.WriteLine("start to dress");
        Thread.Sleep(3000);
        Console.WriteLine("dressed");
    }
    static void brekfast()
    {
        Console.WriteLine("eating");
        Thread.Sleep(10000);
        Console.WriteLine("eated");
    }
    static void podcast()
    {
        Console.WriteLine("podcast on");
        Thread.Sleep(10000);
        Console.WriteLine("podcast off");
    }
    static void Main(string[] args)
    {
            //Console.WriteLine("HelloWorld");
            ////Thread thread = new Thread(Go);
            ////thread.Start();
            ////Go();

            //Thread T_a = new Thread(aa);
            //Thread T_b = new Thread(bb);

            //T_a.Start();
            //T_b.Start();

            //T_a.Join();
            //T_b.Join();


            //Console.WriteLine("END OF PROGRAM");
            Console.WriteLine("THE DAY STARTS");
            Console.WriteLine("Dayly morning routine");
            Thread wake = new Thread(wakeup);
            Thread musicon = new Thread(music);
            Thread podcaston = new Thread(podcast);
            Thread breakfaston = new Thread(brekfast);
            Thread sh = new Thread(shower);
            Thread dress = new Thread(dressup);
            
            wake.Start();
            musicon.Start();
            wake.Join();
            sh.Start();
            sh.Join();
            dress.Start();
            dress.Join();
            musicon.Join();
            podcaston.Start();
                breakfaston.Start();
        breakfaston.Join();
        podcaston.Join();
            

        Console.WriteLine("END of morning routine");


    }
}
