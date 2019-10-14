using System;

namespace Fase4
{
    class MainClass
    {
        const int anyInicial = 1948;
        const int anyTraspas = 4;
        public static void Main(string[] args)
        {
            //FASE 4
            Console.WriteLine("\nFASE 4");

            String nom = "Cristian\t";
            String cognom1 = "Puntí\t";
            String cognom2 = "Sabatés\t";

            int dia = 25;
            int mes = 9;
            int any = 1984;

            bool anyNaixTras;
            int i = anyInicial;


            String nomCognoms = nom + cognom1 + cognom2;
            String dataNaix = dia + "/" + mes + "/" + any;

            anyNaixTras = any % anyTraspas == 0 && i % 100 != 0 || i % 400 == 0;

            Console.WriteLine("El meu nom és " + nomCognoms);
            Console.WriteLine("Vaig néixer el " + dataNaix);


            if (anyNaixTras)
            {
                Console.WriteLine("El meu any de naixement és de traspàs");
            }
            else
            {
                Console.WriteLine("El meu any de naixement no és de traspàs");
            }
        }
    }
}
