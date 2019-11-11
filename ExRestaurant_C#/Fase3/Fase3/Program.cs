using System;
using System.Collections.Generic;
using System.Linq;

namespace Fase3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Fase 3\n");

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
            Console.WriteLine("\nAfegim el preu de cada plat:\n ");

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
                d = "";
                while (d != "1" && d != "0")
                {
                    Console.WriteLine("\nVol seguir demanant menjar? (1:Si / 0:No)");
                    d = Console.ReadLine();
                }

                int j = Convert.ToInt32(d);

                if (j == 0)
                {
                    break;
                }

            }

            foreach (var valor in menjar)
            {
                for (int p = 0; p < length1; p++)
                {
                    if (valor.Contains(menu[p]))
                    {
                        preuTotal += preu[p];
                        Console.WriteLine("Aquest és el plat {0} i val {1}", valor, preu[p]);

                        break;

                    }
                    else if (p == length1 - 1)
                    {
                        Console.WriteLine("El producte {0} que ha demanat no existeix.", valor);

                    }
                }
            }
            Console.WriteLine("El preu total és: " + preuTotal + "\n");

            int ce = 0;
            int ci = 0;
            int v = 0;
            int de = 0;
            int cinc = 0;
            int cc = 0;
            int dc = 0;

            if (preuTotal >= bCincCents)
            {
                cc = (int)preuTotal / bCincCents;
                preuTotal -= cc * bCincCents;
            }

            if (preuTotal >= bDosCents)
            {
                dc = (int)preuTotal / bDosCents;
                preuTotal -= dc * bDosCents;
            }

            if (preuTotal >= bCent)
            {
                ce = (int)preuTotal / bCent;
                preuTotal -= (ce * bCent);
            }

            if (preuTotal >= bCinquanta)
            {
                ci = (int)preuTotal / bCinquanta;
                preuTotal -= ci * bCinquanta;
            }

            if (preuTotal >= bVint)
            {
                v = (int)preuTotal / bVint;
                preuTotal -= v * bVint;
            }

            if (preuTotal >= bDeu)
            {
                de = (int)preuTotal / bDeu;
                preuTotal -= de * bDeu;
            }

            if (preuTotal >= bCinc)
            {
                cinc = (int)preuTotal / bCinc;
                _ = cinc * bCinc;
            }
          
            Console.WriteLine("Bitllets de 500: " + cc);
            Console.WriteLine("Bitllets de 200: " + dc);
            Console.WriteLine("Bitllets de 100: " + ce);
            Console.WriteLine("Bitllets de 50: " + ci);
            Console.WriteLine("Bitllets de 20: " + v);
            Console.WriteLine("Bitllets de 10: " + de);
            Console.WriteLine("Bitllets de 5: " + cinc);

            Console.ReadKey();

        }
    }
}
