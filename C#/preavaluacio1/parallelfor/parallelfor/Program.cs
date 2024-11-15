
//En una frase expliqueu-me què és i perquè serveix el Parallel.For. (No sigueu ganduls i feu-la vosaltres, no el GPT)

//Parallel.For
//es una altra manera de crear threads amb la diferencia de que amb Parallel.for es fa en bucle i no s'ha de declarar ningún thread en el process
//elque ens permet crear una major quantitat de threats sense saturar el codi amb Threads creats normalment (Thread si = new Thread(SuperFuncio);)
//aprofitant millor la memoria i la CPU

using System.Threading;

namespace Exemple_ParallelForEach
{
    class CustomClass
    {
        public string AtributA { get; set; }
        public int AtributB { get; set; }
        public CustomClass(string atributA, int atributB)
        {
            AtributA = atributA;
            AtributB = atributB;
        }
    }
    class Program
    {
        static List<CustomClass> colleccioCustomClass = new List<CustomClass>();
        static void Main(string[] args)
        {
            Inicialitza();
            Parallel.ForEach(colleccioCustomClass, delegate (CustomClass C)
            {
                int threadid = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"AtributA {C.AtributA} \t ThreadID: {threadid}");
                Console.WriteLine($"AtributB {C.AtributB} \t ThreadID: {threadid}");
                Console.WriteLine($"end {threadid}");
            });
            
        }
        static void Inicialitza()
        {
            for (int i = 0; i < 20; i++)
            {
                CustomClass myCustomClass = new CustomClass(i.ToString(), i);
                colleccioCustomClass.Add(myCustomClass);
            }
        }
    }
}