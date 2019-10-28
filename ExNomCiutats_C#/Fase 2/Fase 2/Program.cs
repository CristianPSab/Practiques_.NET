using System;

namespace Fase_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fase 2\n");
            Console.WriteLine("Introdueix-hi els següents noms:\n");

            while (true)
            {

                string a = Console.ReadLine();
                string b = Console.ReadLine();
                string c = Console.ReadLine();
                string d = Console.ReadLine();
                string e = Console.ReadLine();
                string f = Console.ReadLine();

                //Declarem i omplim l'array
                string[] arrayCiutats = new string[] { a, b, c, d, e, f };

                //Calculem el tamany de l'array
                int length = arrayCiutats.Length;

                //Ordenar Array de manera ascendent
                Array.Sort(arrayCiutats);

                Console.Write("\nLes 6 ciutats introduïdes són: ");

                for (int i = 0; i < length; i++)
                {
                    if (arrayCiutats.Length - 2 == i)
                    {
                        Console.Write(arrayCiutats[i] + " i ");

                    }
                    else if (arrayCiutats.Length - 1 == i)
                    {
                        Console.Write(arrayCiutats[i] + ". ");

                    }
                    else
                    {
                        Console.Write(arrayCiutats[i] + " , ");

                    }
                }
                Console.ReadLine();
                break;
            }
        }
    }
}
