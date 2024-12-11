using System;

namespace PlanificadorProcessos
{
    class suma_numeros {
        public suma_numeros(int suma, int vegades, int sleep) {
            sumatori = suma;
            repeticions = vegades;
            pausa = sleep;
        }
        public int sumatori { get; set; }
        public int repeticions { get; set; }
        public int  pausa { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int num_a_sumar = Convert.ToInt32(args[0]);
            int num_vegades_sumar = Convert.ToInt32(args[1]);
            int num_pausa_entre_suma = Convert.ToInt32(args[2]);
            suma_numeros suma = new suma_numeros(num_a_sumar, num_vegades_sumar, num_pausa_entre_suma);
            Thread sumaNum = new Thread(funcionalitat);
            sumaNum.Start(suma);


        }

        static void funcionalitat(object parameters) {
            Console.WriteLine("Calculando...");
            suma_numeros suma_numerosobj = (suma_numeros)parameters;
            int aux = 0;
            for (int i = 0; i < suma_numerosobj.repeticions; i++) {
                int auxiliar_suma = aux;
                aux += suma_numerosobj.sumatori;
                Console.WriteLine("{0} + {1} = {2}", auxiliar_suma, suma_numerosobj.sumatori, aux);
                Thread.Sleep(suma_numerosobj.pausa);
            }
            
        }
    }
}
