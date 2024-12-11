using System.Diagnostics;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main(string[] args)
    {
        Process P = new Process();
        Boolean program = true;
        P.StartInfo.FileName = @"..\processosINICI.exe";
        
        while (program) {
            Console.WriteLine("\n\nsuper printador de lletres\n" +
                "necessitare que introdueixis 2 parametres:\n" +
                "\n" +
                "1- introdueix siusplau una lletra");
            ConsoleKeyInfo tecla = Console.ReadKey(); // Lee un carácter
            char caracter = tecla.KeyChar;
            Console.WriteLine("\n2- introdueix les vegades que vols que es repeteixi el caracter");
            string aux = Console.ReadLine();
            int times;
            int.TryParse(aux,out times);
            char opcion;
            do
            {
                Console.WriteLine("\nVols executar el Process? (S/N)");
                tecla = Console.ReadKey(); // Lee una tecla
                opcion = char.ToUpper(tecla.KeyChar); // Convierte a mayúscula para aceptar 's' o 'n'
                Console.Clear();
                if (opcion != 'S' && opcion != 'N')
                {
                    Console.WriteLine("\nOpció no vàlida. Si us plau, introdueix 'S' o 'N'.");
                }

            } while (opcion != 'S' && opcion != 'N');
            aux = caracter + " " + times;
            if (opcion == 'S') {
                P.StartInfo.Arguments = aux;
                P.StartInfo.UseShellExecute = true;
                P.StartInfo.CreateNoWindow = true;
                P.Start();
            }

        }
        

    }
}