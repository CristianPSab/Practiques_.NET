using System;
using System.Collections.Generic;

namespace Fase_2
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Fase 2\n");

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

            int length = length1 + length2;
            int i = 0;

            for (; i < length - 5; i++)
            {
                Console.WriteLine("\n" + menu[i] + ":\t" + preu[i] + "\teuros.");
            }
            Console.ReadLine();

            List<String> menjar = new List<String>();

            while (true)
            {
                String d;
                String m;
                Console.WriteLine("Que vol per menjar?\n");
                m = Console.ReadLine();
                menjar.Add(m);

                Console.WriteLine("\nHa demanat " + m + "\n");


                int j = 0;
                d = "";
                while (d != "1" && d != "0")
                {
                    Console.WriteLine("\nVol seguir demanant menjar? (1:Si / 0:No)");
                    d = Console.ReadLine();
                }

                j = Convert.ToInt32(d);

                if (j == 0)
                {
                    break;
                }
            }
        }
    }
}
