using System;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace ex3
{
    class main
    {
        //per sincronització de tasques
        static ManualResetEvent tasca1 = new ManualResetEvent(false);
       
        static ManualResetEvent tasca4 = new ManualResetEvent(false);
        static ManualResetEvent tasca5 = new ManualResetEvent(false);

        public static void Tasca1()
        {
            System.Console.WriteLine("Inici tasca 1");
            Thread.Sleep(4000);
            System.Console.WriteLine("Fi tasca 1");
            tasca1.Set(); //senyal per tasca 2 i 3
        }
        public static void Tasca2()
        {
            tasca1.WaitOne();//espera a que la tasca 1 acabi i doni la señal
            System.Console.WriteLine("Inici tasca 2");
            Thread.Sleep(4000);
            System.Console.WriteLine("Fi tasca 2");
            tasca4.Set();
        }
        public static void Tasca3()
        {
            tasca1.WaitOne();//espera a que la tasca 1 acabi i doni la señal
            System.Console.WriteLine("Inici tasca 3");
            Thread.Sleep(4000);
            System.Console.WriteLine("Fi tasca 3");
            tasca5.Set();
        }
        public static void Tasca4()
        {
            tasca4.WaitOne(); //espera a la senyal que s'han acabat les tasques 2 i 3
            System.Console.WriteLine("Inici tasca 4");
            Thread.Sleep(4000);
            System.Console.WriteLine("Fi tasca 4");
        }
        public static void Tasca5()
        {
            tasca5.WaitOne(); //espera a la senyal que s'han acabat les tasques 2 i 3
            System.Console.WriteLine("Inici tasca 5");
            Thread.Sleep(4000);
            System.Console.WriteLine("Fi tasca 5");
        }
        public static void Main(string[] args)
        {
            //¿Qué es CountdownEvent?

            //CountdownEvent es un objeto de sincronización que permite esperar a que se completen múltiples
            //operaciones(tareas/ hilos). Funciona con un contador inicial(en este caso, 2), y cada vez que una
            //operación(como una tarea o hilo) termina, se llama al método Signal() para decrementar ese contador.
            //Cuando el contador llega a 0, significa que todas las operaciones esperadas han finalizado, y cualquier
            //hilo que esté esperando en countdown.Wait() puede continuar.

            //CountdownEvent count = new CountdownEvent(2);

            Thread t1 = new Thread(Tasca1);
            //Thread t2 = new Thread(() => { Tasca2(); count.Signal(); });
            //Thread t3 = new Thread(() => { Tasca3(); count.Signal(); }); //cuan s'activi el thread decrementara el countdown per 1
            Thread t2 = new Thread(Tasca2);
            Thread t3 = new Thread(Tasca3);
            Thread t4 = new Thread(Tasca4);
            Thread t5 = new Thread(Tasca5);


            t1.Start();
            //una vegada acabi tasca1 dona la senyal per a que començin 2 i 3 degut al WaitOne
            t2.Start();
            t3.Start();

            //count.Wait(); //posa en espera tots els threads actius en el moment fins que el countdown sigui 0

            

            t4.Start();
            t5.Start();


            //es junten els threads al principal
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
        }
    }
}