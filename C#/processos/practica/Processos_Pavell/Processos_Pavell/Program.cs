using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Client {
    class Client
    {
        static void Main(string[] args)
        {
            String nom = args[0];  //recoleccio de dades sobre el nom
            String cognom1 = args[1];  //recoleccio de dades sobre el cognom
            int curs = Convert.ToInt32(args[2]); //recoleccio de dades sobre el curs que es fa
            
            Random rnd = new Random();
            Console.WriteLine("+---------------------------------------+\n" +
                "|\tAlumne: {0} {1}\t\t|\n" +
                "| \t\t\t\t\t|\n" +
                "| \t\t\t\t\t|\n" +
                "|\tCurs: DAM{2}\t\t\t|\n" +
                "| \t\t\t\t\t|\n" +
                "|\tM01: {3}\t\t\t\t|\n" +
                "|\tM02: {4}\t\t\t\t|\n" +
                "|\tM03: {5}\t\t\t\t|\n" +
                "|\tM04: {6}\t\t\t\t|\n" +
                "|\tM05: {7}\t\t\t\t|\n" +
                "|\tM06: {8}\t\t\t\t|\n" +
                "|\tM07: {9}\t\t\t\t|\n" +
                "|\tM08: {10}\t\t\t\t|\n" +
                "|\tM09: {11}\t\t\t\t|\n" +
                "|\tM010: {12}\t\t\t\t|\n" +
                "+---------------------------------------+", nom, cognom1, curs
                , rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11), 
                rnd.Next(1, 11));
            Thread.Sleep(20000);

        }

        
    }
}