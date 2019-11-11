using System;

namespace Fase_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fase 1\n");

            int bCinc = 5;
            int bDeu = 10;
            int bVint = 20;
            int bCinquanta = 50;
            int bCent = 100;
            int bDosCents = 200;
            int bCincCents = 500;
            double preuTotal = 0;

            string[] menu = new String[5];
            double[] preu = new Double[5];

            int length1 = menu.Length;
            int length2 = preu.Length;

            Console.WriteLine("Afegim els 5 plats del menú:\n ");

            for (int mi = 0; mi < length1; mi++)
            {
                String m;
                m = Console.ReadLine();
                menu[mi] = m;
            }
            Console.WriteLine("\nAfegim els 5 preus de cada plat:\n ");

            for (int pi = 0; pi < length2; pi++)
            {

                String p;
                p = Console.ReadLine();
                try
                {

                    preu[pi] = double.Parse(p);
                }
                catch (FormatException)
                {
                    Console.WriteLine("El preu ha de ser numèric");
                    pi--;
                }
            }

            Console.ReadLine();
        }
    }
}
